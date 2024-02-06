using FluentValidation;
using MSP.Auth.API.DTOs;

namespace MSP.Clients.API.Validations;

public class ClientRequestValidator : AbstractValidator<ClientRequestDTO>
{
    public ClientRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.DocumentNumber)
            .NotEmpty()
            .NotNull()
            .Length(11);
    }
}