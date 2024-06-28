using Carter;
using Carter.OpenApi;
using MSP.Courses.API.Features.Category.DTOs;
using MSP.Courses.API.Features.Category.Mappers;
using MSP.Courses.Domain.Interfaces;

namespace MSP.Courses.API.Features.Category.Routes;

public class GetCategory : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/categories", async (ICategoryRepository repository) 
                => await HandleGetCategoriesAsync(repository))
            .WithName(nameof(GetCategory))
            .WithTags(nameof(Domain.Entities.Category))
            .IncludeInOpenApi();

    }

    private async Task<IEnumerable<GetCategoryResponseDTO>> HandleGetCategoriesAsync(ICategoryRepository repository)
    {
        var response = await repository.GetAllAsync();
        return response.ToDTO();
    }
}