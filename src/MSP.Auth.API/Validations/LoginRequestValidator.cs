using FluentValidation;
using MSP.Auth.API.DTOs;

namespace MSP.Auth.API.Validations;

public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull()
            .MaximumLength(255);
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255);
    }
}