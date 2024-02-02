using MSP.Core.Models;

namespace MSP.Auth.API.Models;

public class AuthUserRole : Enumeration<AuthUserRole>
{
    public static AuthUserRole Admin = new AuthUserRole(1, "Admin");
    public static AuthUserRole Professor = new AuthUserRole(2, "Professor");
    public static AuthUserRole Student = new AuthUserRole(3, "Student");
    public static AuthUserRole Guest = new AuthUserRole(4, "Guest");

    public AuthUserRole(int id, string name) : base(id, name)
    {
    }
}