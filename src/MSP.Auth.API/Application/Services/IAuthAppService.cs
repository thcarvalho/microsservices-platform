﻿using MSP.Auth.API.ViewModels;

namespace MSP.Auth.API.Application.Services;

public interface IAuthAppService
{
    Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request);
    Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request);
}