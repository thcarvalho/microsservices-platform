using Carter;
using Carter.OpenApi;
using MSP.Courses.API.Features.Category.DTOs;
using MSP.Courses.API.Features.Category.Mappers;
using MSP.Courses.Domain.Interfaces;

namespace MSP.Courses.API.Features.Category.Routes;

public class GetCategoryById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/categories/{id:length(24)}", async (ICategoryRepository repository, string id)
                => await HandleGetCategoryByIdAsync(repository, id))
            .WithName(nameof(GetCategoryById))
            .WithTags(nameof(Domain.Entities.Category))
            .IncludeInOpenApi();
    }

    private async Task<GetCategoryResponseDTO> HandleGetCategoryByIdAsync(ICategoryRepository repository, string id)
    {
        var response = await repository.GetByIdAsync(id);
        return response.ToDTO();
    }
}