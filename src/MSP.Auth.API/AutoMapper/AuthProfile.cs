using AutoMapper;
using MSP.Auth.API.Models;
using MSP.Auth.API.ViewModels;
using MSP.Core.Integration;

namespace MSP.Auth.API.AutoMapper;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<AuthUser, RegisterResponseDTO>();
        CreateMap<AuthUser, ClientRegisteredIntegrationEvent>()
            .ForMember(ie => ie.RoleId, src => src.MapFrom(m => m.AuthUserRoleId));
        CreateMap<RegisterRequestDTO, AuthUser>();
    }
}