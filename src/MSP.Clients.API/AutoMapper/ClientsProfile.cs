using AutoMapper;
using MSP.Clients.API.Enums;
using MSP.Clients.API.Models;
using MSP.Core.Integration;

namespace MSP.Clients.API.AutoMapper;

public class ClientsProfile : Profile
{
    public ClientsProfile()
    {
        CreateMap<ClientRegisteredIntegrationEvent, Client>();
    }
}