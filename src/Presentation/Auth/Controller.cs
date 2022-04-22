using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Data;
using Domain;
using Domain.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Auth;

[Route("auth")]
public class Controller : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly UserManager<MApplicationUser> _userManager;
    private readonly Context _context;
    private readonly ITokenService _tokenService;

    public Controller(UserManager<MApplicationUser> userManager, Context context, ITokenService tokenService)
    {
        _userManager = userManager;
        _context = context;
        _tokenService = tokenService;
    }
    
    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody]Credentials credentials)
    {
        MApplicationUser user = new(){ UserName = credentials.UserName };
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

            var token =  _tokenService.GenerateAccessToken(authClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(1);
            await _userManager.UpdateAsync(user);
            
            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = refreshToken,
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }
    
    [Route("refresh")]
    [HttpPost]
    public async Task<IActionResult> RefreshToken([FromBody] MAuthTokens tokens)
    {
        if (tokens is null)
        {
            return BadRequest("Invalid client request");
        }

        string? accessToken = tokens.AccessToken;
        string? refreshToken = tokens.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        #pragma warning disable CS8602 // Dereference of a possibly null reference.
        
        string username = principal.Identity.Name;
        
        #pragma warning restore CS8602 // Dereference of a possibly null reference.
        #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        var user = await _userManager.FindByNameAsync(username);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList());
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }
}