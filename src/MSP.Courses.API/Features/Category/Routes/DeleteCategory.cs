using Carter;
using Carter.OpenApi;
using MSP.Core.Models;
using MSP.Courses.Domain.Interfaces;
using MSP.WebAPI.Models;
using MSP.WebAPI.Services;

namespace MSP.Courses.API.Features.Category.Routes;

public class DeleteCategory : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/categories/{id:length(24)}", async (
                    HttpContext context,
                    ICategoryRepository repository,
                    INotificationCollector notificationCollector,
                    string id)
                => ApiResponseFactory.CreateBaseResponse(await HandleDeleteCategoryAsync(id, repository, notificationCollector), notificationCollector, context))
            .WithName(nameof(DeleteCategory))
            .WithTags(nameof(Domain.Entities.Category))
            .IncludeInOpenApi();
    }

    private async Task<bool> HandleDeleteCategoryAsync(
        string id, 
        ICategoryRepository repository,
        INotificationCollector notificationCollector)
    {
        if (await ExistsCategoryByIdAsync(id, repository, notificationCollector)) return default;
        return repository.DeleteById(id);
    }
    
    private async Task<bool> ExistsCategoryByIdAsync(
        string id, 
        ICategoryRepository repository, 
        INotificationCollector notificationCollector)
    {
        var exists = await repository.ExistsByIdAsync(id);
        if (exists) notificationCollector.AddNotification(new ErrorResponse(nameof(Category), "Category not exists."));
        return exists;
    }
}