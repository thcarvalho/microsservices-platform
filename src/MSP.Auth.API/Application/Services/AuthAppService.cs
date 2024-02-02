using System.Security.Claims;
using AutoMapper;
using Azure;
using FluentValidation;
using MSP.Auth.API.Data;
using MSP.Auth.API.Data.Repositories;
using MSP.Auth.API.Models;
using MSP.Auth.API.Utils;
using MSP.Auth.API.ViewModels;
using MSP.Core.Integration;
using MSP.Core.Models;
using MSP.Data.Core;
using MSP.MessageBus;
using MSP.WebAPI.Models;
using MSP.WebAPI.Services;

namespace MSP.Auth.API.Application.Services;

public class AuthAppService : BaseAppService, IAuthAppService
{
    private readonly IAuthUserRepository _authUserRepository;
    private readonly IMessageBus _bus;
    private readonly IMapper _mapper;
    private readonly IAuthUnitOfWork _unitOfWork;
    private readonly IValidator<LoginRequestDTO> _loginValidator;
    private readonly IValidator<RegisterRequestDTO> _registerValidator;
    private readonly ITokenGeneratorService _tokenGeneratorService;

    public AuthAppService(
        INotificationCollector notifications,
        IHttpContextAccessor httpContextAccessor,
        IAuthUserRepository authUserRepository,
        IMessageBus bus,
        IMapper mapper,
        IAuthUnitOfWork unitOfWork,
        IValidator<LoginRequestDTO> loginValidator,
        IValidator<RegisterRequestDTO> registerValidator,
        ITokenGeneratorService tokenGeneratorService) : base(notifications, httpContextAccessor)
    {
        _authUserRepository = authUserRepository;
        _bus = bus;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
        _tokenGeneratorService = tokenGeneratorService;
    }

    public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request)
    {
        var validation = await _loginValidator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            _notificationsCollector.AddNotifications(validation);
            return default;
        }

        var user = await _authUserRepository.GetFirstAsync(x => x.Email == request.Email, "Role");
        if (!PasswordHasher.Verify(request.Password, user.PasswordHash))
        {
            _notificationsCollector.AddNotification(new ErrorResponse(nameof(LoginRequestDTO), "User or password not found."));
            return default;
        }

        var claims = new List<Claim>
        {
            new ("id", user.Id.ToString()),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.Name, user.Name),
            new (ClaimTypes.Role, user.Role.Name)
        };
        return new LoginResponseDTO { Token = _tokenGeneratorService.GenerateToken(claims) };
    }

    public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request)
    {
        var validation = await _registerValidator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            _notificationsCollector.AddNotifications(validation);
            return default;
        }

        var authUser = new AuthUser(request.Name, request.Email, request.Password, AuthUserRole.Guest.Id);
        authUser.CreateRegister(request.Password);
        var response = await _authUserRepository.CreateAsync(authUser);
        await _unitOfWork.CommitAsync();

        var busMessage = _mapper.Map<ClientRegisteredIntegrationEvent>(response);
        busMessage.DocumentNumber = request.DocumentNumber;

        try
        {
            var messageResponse = await _bus.RequestAsync<ClientRegisteredIntegrationEvent, MessageResponse>(busMessage);
            if (!messageResponse.IsValid)
                await RollbackRegisterAsync(response);
        }
        catch (TaskCanceledException)
        {
            await RollbackRegisterAsync(response);
        }

        return _mapper.Map<RegisterResponseDTO>(response);
    }

    private async Task RollbackRegisterAsync(AuthUser user)
    {
        _authUserRepository.DeleteOne(user);
        await _unitOfWork.CommitAsync();
    }
}