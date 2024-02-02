using AutoMapper;
using FluentValidation;
using MSP.Clients.API.Data;
using MSP.Clients.API.Data.Repositories;
using MSP.Clients.API.Models;
using MSP.Core.Integration;
using MSP.Core.Models;
using MSP.MessageBus;
using MSP.WebAPI.Services;

namespace MSP.Clients.API.Application.Services;

public class ClientAppService : BaseAppService, IClientAppService
{
    private readonly IClientRepository _clientRepository;
    private readonly IClientsUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<ClientRegisteredIntegrationEvent> _userRegisteredValidator;

    public ClientAppService(
        INotificationCollector notifications, 
        IHttpContextAccessor httpContextAccessor, 
        IClientRepository clientRepository, 
        IClientsUnitOfWork unitOfWork, 
        IMapper mapper, 
        IValidator<ClientRegisteredIntegrationEvent> userRegisteredValidator) : base(notifications, httpContextAccessor)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRegisteredValidator = userRegisteredValidator;
    }

    public async Task<MessageResponse> CreateAsync(ClientRegisteredIntegrationEvent message)
    {
        var validation = await _userRegisteredValidator.ValidateAsync(message);
        if (!validation.IsValid) 
            return GetMessageResponse(validation);

        var client = new Client(message.Name, message.DocumentNumber, message.Email);
        await _clientRepository.CreateAsync(client);
        await _unitOfWork.CommitAsync();

        return new MessageResponse();
    }
}