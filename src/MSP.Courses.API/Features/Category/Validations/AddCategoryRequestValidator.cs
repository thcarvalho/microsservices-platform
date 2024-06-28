using FluentValidation;
using MSP.Courses.API.Features.Category.DTOs;

namespace MSP.Courses.API.Features.Category.Validations;

public class AddCategoryRequestValidator : AbstractValidator<AddCategoryRequestDTO>
{
    public AddCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3);
    }
}