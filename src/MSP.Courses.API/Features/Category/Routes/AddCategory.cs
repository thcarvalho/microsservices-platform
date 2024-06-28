using Carter;
using Carter.OpenApi;
using FluentValidation;
using MSP.Core;
using MSP.Core.Models;
using MSP.Courses.API.Features.Category.DTOs;
using MSP.Courses.API.Features.Category.Mappers;
using MSP.Courses.Domain.Interfaces;
using MSP.WebAPI.Models;
using MSP.WebAPI.Services;

namespace MSP.Courses.API.Features.Category.Routes;

public class AddCategory : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/categories", async (
                    HttpContext context,
                    IValidator<AddCategoryRequestDTO> validator,
                    ICategoryRepository repository,
                    INotificationCollector notificationCollector,
                    AddCategoryRequestDTO request)
                => ApiResponseFactory.CreateBaseResponse(await HandleAddCategoryAsync(request, validator, repository, notificationCollector), notificationCollector, context))
            .WithName(nameof(AddCategory))
            .WithTags(nameof(Domain.Entities.Category))
            .IncludeInOpenApi();
    }

    private async Task<GetCategoryResponseDTO?> HandleAddCategoryAsync(
        AddCategoryRequestDTO request, 
        IValidator<AddCategoryRequestDTO> validator, 
        ICategoryRepository repository, 
        INotificationCollector notificationCollector)
    {
        if (!await IsValidDTOAsync(request, validator, notificationCollector) || await ExistsCategoryAsync(request.Name, repository, notificationCollector))
            return default;

        var response = await repository.CreateAsync(request.ToEntity());
        return response.ToDTO();
    }

    private async Task<bool> IsValidDTOAsync(
        AddCategoryRequestDTO dto, 
        IValidator<AddCategoryRequestDTO> validator, 
        INotificationCollector notificationCollector)
    {
        var validation = await validator.ValidateAsync(dto);
        var isValid = validation.IsValid;
        if (!isValid) notificationCollector.AddNotifications(validation.Errors);
        return isValid;
    }

    private async Task<bool> ExistsCategoryAsync(
        string name, 
        ICategoryRepository repository, 
        INotificationCollector notificationCollector)
    {
        var exists = await repository.ExistsAsync(x => x.Name.ToUpper().Equals(name.ToUpper()));
        if (exists) notificationCollector.AddNotification(new ErrorResponse(nameof(Category), "Category already exists."));
        return exists;
    }
}