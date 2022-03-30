using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Auth;

[Route("auth")]
public class Controller : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public Controller(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    
    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody]Credentials credentials)
    {
        IdentityUser user = new IdentityUser(){ UserName = credentials.UserName };
        var result = await _userManager.CreateAsync(user, credentials.Password);
        var statusCode = result.Succeeded ? 200 : 400;

        return StatusCode(statusCode, result);
    }

    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] Credentials credentials)
    {
        var user = await _userManager.FindByNameAsync(credentials.UserName);
        
        if (user != null && await _userManager.CheckPasswordAsync(user, credentials.Password))
        {

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = GetToken(authClaims);
            var refreshToken = GenerateRefreshToken();
            
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = refreshToken,
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }
    
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var bytes = Encoding.UTF8.GetBytes("this is my custom Secret key for authentication");
        var authSigningKey = new SymmetricSecurityKey(bytes);

        var token = new JwtSecurityToken(
            issuer: "https://localhost:7216/",
            audience: "https://localhost:7216/",
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
    
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rngRandomNumberGenerator = RandomNumberGenerator.Create();
        rngRandomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}