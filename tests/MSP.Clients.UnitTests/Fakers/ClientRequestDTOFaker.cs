using MSP.Auth.API.DTOs;
using MSP.UnitTests.Core;

namespace MSP.Clients.UnitTests.Fakers;

public class ClientRequestDTOFaker : ObjectFaker<ClientRequestDTO>
{
    public ClientRequestDTOFaker()
    {
        UsePrivateConstructor()
            .RuleFor(x => x.DocumentNumber, f => f.Random.ReplaceNumbers("###########"))
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.Name, f => f.Name.FullName());
    }
}