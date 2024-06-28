using Carter;
using Carter.OpenApi;
using FluentValidation;
using MSP.Core.Models;
using MSP.Courses.API.Features.Category.DTOs;
using MSP.Courses.API.Features.Category.Mappers;
using MSP.Courses.Domain.Interfaces;
using MSP.WebAPI.Models;
using MSP.WebAPI.Services;

namespace MSP.Courses.API.Features.Category.Routes;

public class UpdateCategory : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/categories/{id:length(24)}", async (
                    HttpContext context, 
                    IValidator<UpdateCategoryRequestDTO> validator,
                    ICategoryRepository repository, 
                    INotificationCollector notificationCollector, 
                    UpdateCategoryRequestDTO request, 
                    string id)
                => ApiResponseFactory.CreateBaseResponse(
                    await HandleUpdateCategoryAsync(repository, notificationCollector, validator, request, id), 
                    notificationCollector, 
                    context))
            .WithName(nameof(UpdateCategory))
            .WithTags(nameof(Domain.Entities.Category))
            .IncludeInOpenApi();
    }

    private async Task<GetCategoryResponseDTO?> HandleUpdateCategoryAsync(
        ICategoryRepository repository,
        INotificationCollector notificationCollector,
        IValidator<UpdateCategoryRequestDTO> validator,
        UpdateCategoryRequestDTO request,
        string id)
    {
        if (!await IsValidDTOAsync(request, notificationCollector, validator) || 
            !await ExistsCategoryByIdAsync(id, repository, notificationCollector) || 
             await ExistsCategoryByNameAsync(repository, notificationCollector, request.Name, id))
            return default;

        var current = await repository.GetByIdAsync(id);
        current.UpdateName(request.Name);
        repository.Update(current);

        return current.ToDTO();
    }

    private async Task<bool> IsValidDTOAsync(
        UpdateCategoryRequestDTO dto, 
        INotificationCollector notificationCollector,
        IValidator<UpdateCategoryRequestDTO> validator)
    {
        var validation = await validator.ValidateAsync(dto);
        var isValid = validation.IsValid;
        if (!isValid) notificationCollector.AddNotifications(validation.Errors);
        return isValid;
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

    private async Task<bool> ExistsCategoryByNameAsync(
        ICategoryRepository repository,
        INotificationCollector notificationCollector,
        string name, 
        string id)
    {
        var exists = await repository.ExistsAsync(x => x.Name.ToUpper().Equals(name.ToUpper()) && !x.Id.Equals(id));
        if (exists) notificationCollector.AddNotification(new ErrorResponse(nameof(Category), "Category already exists."));
        return exists;
    }
}