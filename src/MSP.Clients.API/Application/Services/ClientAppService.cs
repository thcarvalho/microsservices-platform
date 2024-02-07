using FluentValidation;
using LinqKit;
using MSP.Clients.API.Data;
using MSP.Clients.API.Data.Repositories;
using MSP.Clients.API.DTOs;
using MSP.Clients.API.Mappers;
using MSP.Clients.API.Models;
using MSP.Clients.API.QueryParams;
using MSP.Core;
using MSP.Core.Models;
using MSP.WebAPI.Models;
using MSP.WebAPI.Services;

namespace MSP.Clients.API.Application.Services;

public class ClientAppService : BaseAppService, IClientAppService
{
    private readonly IClientRepository _clientRepository;
    private readonly IClientsUnitOfWork _unitOfWork;
    private readonly IValidator<ClientRequestDTO> _clientRequestValidator;

    public ClientAppService(
        INotificationCollector notifications, 
        IHttpContextAccessor httpContextAccessor, 
        IClientRepository clientRepository, 
        IClientsUnitOfWork unitOfWork, 
        IValidator<ClientRequestDTO> clientRequestValidator) : base(notifications, httpContextAccessor)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
        _clientRequestValidator = clientRequestValidator;
    }

    public async Task<ClientResponseDTO?> CreateAsync(ClientRequestDTO request)
    {
        if (!await IsValidDTOAsync(request) || 
             await AlreadyExistsAsync(request.Email, request.DocumentNumber))
            return default;

        var response = await _clientRepository.CreateAsync(request.ToEntity());
        await _unitOfWork.CommitAsync();

        return response.ToDTO();
    }

    public async Task<ClientResponseDTO?> UpdateAsync(int id, ClientRequestDTO request)
    {
        if (!await IsValidDTOAsync(request) || 
            !await ExistsClientAsync(id) ||
             await AlreadyExistsAsync(request.Email, request.DocumentNumber, id))
            return default;

        var client = await _clientRepository.GetByIdAsync(id);
        client.UpdateClient(
            name: request.Name,
            documentNumber: request.DocumentNumber,
            email: request.Email);
        var response = _clientRepository.Update(client);

        await _unitOfWork.CommitAsync();
        return response.ToDTO();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await ExistsClientAsync(id)) return default;
        await _clientRepository.DeleteByIdAsync(id);
        return await _unitOfWork.CommitAsync();
    }

    public async Task<ClientResponseDTO?> GetByIdAsync(int id)
    {
        if (!await ExistsClientAsync(id)) return default;
        var client = await _clientRepository.GetByIdAsync(id);
        return client.ToDTO();
    }

    public async Task<BasePaginationResponse<ClientResponseDTO>> GetAsync(ClientQueryParams parameters)
    {
        var clients = await _clientRepository.GetAsync(parameters);
        var count = await _clientRepository.CountAsync(parameters.Filter());
        return clients.ToPagination(count, parameters);
    }

    private async Task<bool> IsValidDTOAsync(ClientRequestDTO dto)
    {
        var validation = await _clientRequestValidator.ValidateAsync(dto);
        var isValid = validation.IsValid;
        if (!isValid) _notificationsCollector.AddNotifications(validation.Errors);
        return isValid;
    }

    private async Task<bool> ExistsClientAsync(int id)
    {
        var exists = await _clientRepository.ExistsByIdAsync(id);
        if (!exists)
            _notificationsCollector.AddNotification(new ErrorResponse(nameof(Client), "Client not exists.", ErrorTypeEnum.NotFound));
        return exists;
    }

    private async Task<bool> AlreadyExistsAsync(string email, string document, int? idExcept = null)
    {
        var predicate = PredicateBuilder.New<Client>();
        predicate = predicate
            .And(x => !idExcept.HasValue || x.Id != idExcept)
            .And(x => x.Email == email)
            .Or(x => x.DocumentNumber == document);

        var exists = await _clientRepository.ExistsAsync(predicate);
        if (exists)
            _notificationsCollector.AddNotification(new ErrorResponse(nameof(Client), "Client already exists"));
        return exists;
    }
}