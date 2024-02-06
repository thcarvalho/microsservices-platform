using Microsoft.AspNetCore.Mvc;
using MSP.Auth.API.Application.Services;
using MSP.Auth.API.DTOs;
using MSP.WebAPI.Controller;
using MSP.WebAPI.Models;
using MSP.WebAPI.Services;

namespace MSP.Auth.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthAppService _authAppService;

        public AuthController(
            INotificationCollector notificationCollector, 
            IAuthAppService authAppService) : base(notificationCollector)
        {
            _authAppService = authAppService;
        }

        [HttpPost("login")]
        public async Task<ApiBaseResponse<LoginResponseDTO>> LoginAsync([FromBody] LoginRequestDTO request)
        {
            return BaseResponse(await _authAppService.LoginAsync(request));
        }

        [HttpPost("register")]
        public async Task<ApiBaseResponse<RegisterResponseDTO>> RegisterAsync([FromBody] RegisterRequestDTO request)
        {
            return BaseResponse(await _authAppService.RegisterAsync(request));
        }
    }
}
