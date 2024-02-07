using System.Security.Claims;
using FluentValidation;
using MSP.Auth.API.Data;
using MSP.Auth.API.Data.Repositories;
using MSP.Auth.API.DTOs;
using MSP.Auth.API.Mapper;
using MSP.Auth.API.Models;
using MSP.Auth.API.Utils;
using MSP.Core.Integration;
using MSP.Core.Models;
using MSP.MessageBus;
using MSP.WebAPI.Services;

namespace MSP.Auth.API.Application.Services;

public class AuthAppService : BaseAppService, IAuthAppService
{
    private readonly IAuthUserRepository _authUserRepository;
    private readonly IMessageBus _bus;
    private readonly IAuthUnitOfWork _unitOfWork;
    private readonly IValidator<LoginRequestDTO> _loginValidator;
    private readonly IValidator<RegisterRequestDTO> _registerValidator;
    private readonly ITokenGeneratorService _tokenGeneratorService;

    public AuthAppService(
        INotificationCollector notifications,
        IHttpContextAccessor httpContextAccessor,
        IAuthUserRepository authUserRepository,
        IMessageBus bus,
        IAuthUnitOfWork unitOfWork,
        IValidator<LoginRequestDTO> loginValidator,
        IValidator<RegisterRequestDTO> registerValidator,
        ITokenGeneratorService tokenGeneratorService) : base(notifications, httpContextAccessor)
    {
        _authUserRepository = authUserRepository;
        _bus = bus;
        _unitOfWork = unitOfWork;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
        _tokenGeneratorService = tokenGeneratorService;
    }

    public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request)
    {
        if (!await IsValidLoginDTOAsync(request) ||
            !await IsLoginValidAsync(request.Email, request.Password))
            return default;

        var user = await _authUserRepository.GetOneAsync(x => x.Email == request.Email, "Role");
        var claims = new List<Claim>
        {
            new ("id", user.Id.ToString()),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.Name, user.Name),
            new (ClaimTypes.Role, user.Role.Name)
        };
        return new LoginResponseDTO { Token = _tokenGeneratorService.GenerateToken(claims) };
    }

    public async Task<RegisterResponseDTO?> RegisterAsync(RegisterRequestDTO request)
    {
        if (!await IsValidRegisterDTOAsync(request) ||
             await UserAlreadyExistsAsync(request.Email))
            return default;

        var authUser = await _authUserRepository.CreateAsync(request.ToAuthUser());
        await _unitOfWork.CommitAsync();

        try
        {
            var messageResponse = await _bus.RequestAsync<ClientRegisteredIntegrationEvent, MessageResponse>(
                authUser.ToClientRegisteredIntegrationEvent(request.DocumentNumber));
            if (!messageResponse.IsValid)
            {
                await RollbackRegisterAsync(authUser);
                _notificationsCollector.AddNotifications(messageResponse.Errors?.ToList());
                return default;
            }
        }
        catch (TaskCanceledException e)
        {
            await RollbackRegisterAsync(authUser);
            _notificationsCollector.AddNotification(new ErrorResponse(nameof(RegisterRequestDTO), e.Message));
            return default;
        }

        return new RegisterResponseDTO
        {
            DocumentNumber = request.DocumentNumber,
            Email = authUser.Email,
            Id = authUser.Id,
            Name = authUser.Name
        };
    }

    private async Task<bool> IsValidLoginDTOAsync(LoginRequestDTO request)
    {
        var validation = await _loginValidator.ValidateAsync(request);
        var isValid = validation.IsValid;
        if (!isValid) _notificationsCollector.AddNotifications(validation.Errors);
        return isValid;
    }

    private async Task<bool> IsValidRegisterDTOAsync(RegisterRequestDTO request)
    {
        var validation = await _registerValidator.ValidateAsync(request);
        var isValid = validation.IsValid;
        if (!isValid) _notificationsCollector.AddNotifications(validation.Errors);
        return isValid;
    }

    private async Task<bool> UserAlreadyExistsAsync(string email)
    {
        var exists = await _authUserRepository.ExistsAsync(x => x.Email == email);
        if (exists)
            _notificationsCollector.AddNotification(new ErrorResponse(nameof(AuthUser), $"User with email '{email}' already exists."));
        return exists;
    }

    private async Task<bool> IsLoginValidAsync(string email, string incomingPassword)
    {
        var user = await _authUserRepository.GetOneAsync(x => x.Email == email);
        if (user is not null)
        {
            var valid = PasswordHasher.Verify(incomingPassword, user.PasswordHash);
            if (valid) return true;
        }
        _notificationsCollector.AddNotification(new ErrorResponse(nameof(AuthUser), "User or password is invalid."));
        return false;
    }

    private async Task RollbackRegisterAsync(AuthUser user)
    {
        _authUserRepository.DeleteOne(user);
        await _unitOfWork.CommitAsync();
    }
}