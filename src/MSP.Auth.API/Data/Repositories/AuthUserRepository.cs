using MSP.Auth.API.Models;
using MSP.Data.Core;

namespace MSP.Auth.API.Data.Repositories;

public class AuthUserRepository : BaseRepository<AuthUser>, IAuthUserRepository
{
    public AuthUserRepository(AuthContext context) : base(context)
    {
    }
}