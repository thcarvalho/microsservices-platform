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
        string passwordHash)
    {
        Name = name;
        Email = email;
        CreateGuestRegister(passwordHash);
    }

    public void CreateGuestRegister(string password)
    {
        PasswordHash = PasswordHasher.Hash(password);
        AuthUserRoleId = AuthUserRole.Guest.Id;
    }
}