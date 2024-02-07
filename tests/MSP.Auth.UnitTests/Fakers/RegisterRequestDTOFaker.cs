using MSP.Auth.API.DTOs;
using MSP.Tests.Shared;

namespace MSP.Auth.UnitTests.Fakers;

public class RegisterRequestDTOFaker : ObjectFaker<RegisterRequestDTO>
{
    public RegisterRequestDTOFaker()
    {
        UsePrivateConstructor()
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.Password, f => f.Internet.Password())
            .RuleFor(x => x.Name, f => f.Name.FullName())
            .RuleFor(x => x.DocumentNumber, f => f.Random.ReplaceNumbers("###########"));
    }
}