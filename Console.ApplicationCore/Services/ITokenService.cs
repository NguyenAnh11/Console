using System.Collections.Generic;
using System.Security.Claims;

namespace Console.ApplicationCore.Services
{
    public interface ITokenService
    {
        string GenerateJSONWebToken(IList<Claim> claims, int expireInSecond);

        ClaimsPrincipal VerifyJSONWebToken(string token);

        string GetAccessToken(IList<Claim> claims);

        string GetRefreshToken(IList<Claim> claims);
    }
}
