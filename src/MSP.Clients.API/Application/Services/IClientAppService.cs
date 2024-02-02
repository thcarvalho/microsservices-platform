using MSP.Core.Integration;
using MSP.Core.Models;

namespace MSP.Clients.API.Application.Services;

public interface IClientAppService
{
    Task<MessageResponse> CreateAsync(ClientRegisteredIntegrationEvent message);
}