using Azure.Core;
using MSP.Auth.API.Utils;
using MSP.Core.Models;

namespace MSP.Auth.API.Models;

public class AuthUser : Entity
{
    public string Name { get; protected set; }
    public string Email { get; protected set; }
    public string PasswordHash { get; protected set; }
    public int AuthUserRoleId { get; protected set; }
    public virtual AuthUserRole Role { get; protected set; }

    protected AuthUser() { }

    public AuthUser(
        string name, 
        string email, 
        string passwordHash, 
        int authUserRoleId)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        AuthUserRoleId = authUserRoleId;
    }


    public void CreateRegister(string password)
    {
        AuthUserRoleId = AuthUserRole.Guest.Id;
        PasswordHash = PasswordHasher.Hash(password);
    }
}