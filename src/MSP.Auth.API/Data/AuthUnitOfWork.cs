using MSP.Data.Core;

namespace MSP.Auth.API.Data;

public class AuthUnitOfWork : UnitOfWork<AuthContext>, IAuthUnitOfWork
{
    public AuthUnitOfWork(AuthContext context) : base(context)
    {
    }
}