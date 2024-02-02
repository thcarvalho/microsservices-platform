using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MSP.Auth.API.Data.Repositories;
using MSP.Auth.API.ViewModels;

namespace MSP.Auth.API.Validations;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestDTO>
{
    public RegisterRequestValidator(IAuthUserRepository authUserRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255);
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull()
            .MaximumLength(255)
            .MustAsync((email, ct) => authUserRepository.AsQueryable().AllAsync(x => x.Email != email))
            .WithMessage("This email already exists.");
        RuleFor(x => x.DocumentNumber)
            .NotEmpty()
            .NotNull()
            .Length(11);
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255);
    }
}