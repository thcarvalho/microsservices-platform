using FluentValidation;
using MSP.Auth.API.Data.Repositories;
using MSP.Auth.API.ViewModels;

namespace MSP.Auth.API.Validations;

public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestValidator(IAuthUserRepository authUserRepository)
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull()
            .MaximumLength(255)
            .MustAsync((email, ct) => authUserRepository.ExistsAsync(x => x.Email == email))
            .WithMessage("User or password not found.");
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255);
    }
}