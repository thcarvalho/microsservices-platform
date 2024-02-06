using Bogus;
using MSP.Auth.API.DTOs;
using MSP.Clients.API.Models;
using MSP.UnitTests.Core;

namespace MSP.Clients.UnitTests.Fakers;

public class ClientFaker : ObjectFaker<Client>
{
    public ClientFaker(ClientRequestDTO? client = null)
    {
        UsePrivateConstructor()
            .RuleFor(x => x.Id, f => f.IndexFaker)
            .RuleFor(x => x.Email, f => client?.Email ?? f.Person.Email)
            .RuleFor(x => x.Name, f => client?.Name ??  f.Person.FullName)
            .RuleFor(x => x.DocumentNumber, f => client?.DocumentNumber ?? f.Random.ReplaceNumbers("###########"))
            .RuleFor(x => x.CreatedAt, f => f.Date.Past())
            .RuleFor(x => x.UpdatedAt, f => f.Date.Past());

    }
}