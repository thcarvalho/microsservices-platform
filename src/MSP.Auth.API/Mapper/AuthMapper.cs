using MSP.Auth.API.DTOs;
using MSP.Auth.API.Models;
using MSP.Core.Integration;

namespace MSP.Auth.API.Mapper;

public static class AuthMapper
{
    public static AuthUser ToAuthUser(this RegisterRequestDTO dto)
    {
        return new AuthUser(dto.Name, dto.Email, dto.Password);
    }

    public static ClientRegisteredIntegrationEvent ToClientRegisteredIntegrationEvent(this AuthUser entity, string documentNumber)
    {
        return new ClientRegisteredIntegrationEvent(
            authUserId: entity.Id,
            name: entity.Name,
            email: entity.Email,
            documentNumber: documentNumber);
    }
}