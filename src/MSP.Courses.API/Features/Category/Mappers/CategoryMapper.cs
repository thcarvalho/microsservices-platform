using MSP.Courses.API.Features.Category.DTOs;

namespace MSP.Courses.API.Features.Category.Mappers;

public static class CategoryMapper
{
    public static Domain.Entities.Category ToEntity(this AddCategoryRequestDTO dto) 
        => new(name: dto.Name);

    public static GetCategoryResponseDTO ToDTO(this Domain.Entities.Category entity) 
        => entity is null 
            ? null
            : new GetCategoryResponseDTO
            {
                Id = entity.Id, 
                Name = entity.Name
            };

    public static IEnumerable<GetCategoryResponseDTO> ToDTO(this IEnumerable<Domain.Entities.Category> entities)
        => entities.Select(ToDTO);
}