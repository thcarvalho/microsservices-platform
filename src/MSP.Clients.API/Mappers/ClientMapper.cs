using MSP.Clients.API.DTOs;
using MSP.Clients.API.Models;
using MSP.Core.Params;
using MSP.WebAPI.Models;

namespace MSP.Clients.API.Mappers;

public static class ClientMapper
{
    public static ClientResponseDTO ToDTO(this Client entity)
        => new()
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Name = entity.Name,
            Email = entity.Email,
            DocumentNumber = entity.DocumentNumber,
        };

    public static Client ToEntity(this ClientRequestDTO dto)
        => new (
            name: dto.Name,
            documentNumber: dto.DocumentNumber,
            email: dto.Email);

    public static BasePaginationResponse<ClientResponseDTO> ToPagination(this IEnumerable<Client> entities, int count, IPaginable pagination)
    {
        return new BasePaginationResponse<ClientResponseDTO>(
            data: entities.Select(ToDTO),
            dataCount: count,
            skip: pagination.Skip!.Value,
            take: pagination.Take!.Value
        );
    }

}