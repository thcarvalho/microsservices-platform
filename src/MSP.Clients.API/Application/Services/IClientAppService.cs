using MSP.Clients.API.DTOs;
using MSP.Clients.API.QueryParams;
using MSP.WebAPI.Models;

namespace MSP.Clients.API.Application.Services;

public interface IClientAppService
{
    Task<ClientResponseDTO?> CreateAsync(ClientRequestDTO request);
    Task<ClientResponseDTO?> UpdateAsync(int id, ClientRequestDTO request);
    Task<bool> DeleteAsync(int id);
    Task<ClientResponseDTO?> GetByIdAsync(int id);
    Task<BasePaginationResponse<ClientResponseDTO>> GetAsync(ClientQueryParams parameters);
}