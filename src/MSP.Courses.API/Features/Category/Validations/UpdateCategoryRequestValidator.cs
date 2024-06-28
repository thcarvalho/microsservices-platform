using FluentValidation;
using MSP.Courses.API.Features.Category.DTOs;

namespace MSP.Courses.API.Features.Category.Validations;

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequestDTO>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3);
    }
}