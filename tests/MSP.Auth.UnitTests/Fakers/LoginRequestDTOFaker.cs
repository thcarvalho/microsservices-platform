using MSP.Auth.API.DTOs;
using MSP.Tests.Shared;

namespace MSP.Auth.UnitTests.Fakers;

public class LoginRequestDTOFaker : ObjectFaker<LoginRequestDTO>
{
    public LoginRequestDTOFaker()
    {
        UsePrivateConstructor()
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.Password, f => f.Internet.Password());
    }
}