using System.Security.Claims;

namespace MSP.Auth.API.Application.Services;

public interface ITokenGeneratorService
{
    string GenerateToken(IEnumerable<Claim> claims);
}