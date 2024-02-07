using Microsoft.AspNetCore.Mvc;
using MSP.Clients.API.Application.Services;
using MSP.Clients.API.DTOs;
using MSP.Clients.API.QueryParams;
using MSP.WebAPI.Controller;
using MSP.WebAPI.Models;
using MSP.WebAPI.Services;

namespace MSP.Clients.API.Controllers
{
    [Route("api/clients")]
    public class ClientsController : BaseApiController
    {
        private readonly IClientAppService _clientAppService;
        public ClientsController(
            INotificationCollector notificationCollector, 
            IClientAppService clientAppService) : base(notificationCollector)
        {
            _clientAppService = clientAppService;
        }

        [HttpPost]
        public async Task<ApiBaseResponse<ClientResponseDTO>> PostAsync([FromBody] ClientRequestDTO request)
            => BaseResponse(await _clientAppService.CreateAsync(request));

        [HttpPut("{id:int}")]
        public async Task<ApiBaseResponse<ClientResponseDTO>> PutAsync(int id, [FromBody] ClientRequestDTO request)
            => BaseResponse(await _clientAppService.UpdateAsync(id, request));

        [HttpDelete("{id:int}")]
        public async Task<ApiBaseResponse<bool>> DeleteAsync(int id)
            => BaseResponse(await _clientAppService.DeleteAsync(id));

        [HttpGet("{id:int}")]
        public async Task<ApiBaseResponse<ClientResponseDTO>> GetByIdAsync(int id)
            => BaseResponse(await _clientAppService.GetByIdAsync(id));

        [HttpGet]
        public async Task<ApiBaseResponse<BasePaginationResponse<ClientResponseDTO>>> GetAsync([FromQuery] ClientQueryParams parameters)
            => BaseResponse(await _clientAppService.GetAsync(parameters));
    }
}
