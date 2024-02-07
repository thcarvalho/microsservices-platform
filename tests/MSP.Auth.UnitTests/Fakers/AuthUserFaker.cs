using MSP.Auth.API.Models;
using MSP.Auth.API.Utils;
using MSP.Tests.Shared;

namespace MSP.Auth.UnitTests.Fakers;

public class AuthUserFaker : ObjectFaker<AuthUser>
{
    public AuthUserFaker(
        string? email = null,
        string? password = null,
        string? name = null,
        int? roleId = null)
    {
        var role = AuthUserRole.GetAll().FirstOrDefault(x => x.Id == (roleId ?? AuthUserRole.Guest.Id));
        UsePrivateConstructor()
            .RuleFor(x => x.Id, f => f.IndexFaker + 1)
            .RuleFor(x => x.Email, f => email ?? f.Person.Email)
            .RuleFor(x => x.Name, f => name ?? f.Person.FullName)
            .RuleFor(x => x.PasswordHash, f => PasswordHasher.Hash(password ?? f.Internet.Password()))
            .RuleFor(x => x.AuthUserRoleId, _ => role?.Id)
            .RuleFor(x => x.Role, _ => role)
            .RuleFor(x => x.CreatedAt, f => f.Date.Past())
            .RuleFor(x => x.UpdatedAt, f => f.Date.Past());
    }
}