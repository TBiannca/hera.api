using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Domain;

public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> authClaims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}