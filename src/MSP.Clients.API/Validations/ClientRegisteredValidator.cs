using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MSP.Clients.API.Data.Repositories;
using MSP.Clients.API.Enums;
using MSP.Core.Integration;

namespace MSP.Clients.API.Validations;

public class ClientRegisteredValidator : AbstractValidator<ClientRegisteredIntegrationEvent>
{
    public ClientRegisteredValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.DocumentNumber)
            .NotEmpty()
            .NotNull();
    }
}