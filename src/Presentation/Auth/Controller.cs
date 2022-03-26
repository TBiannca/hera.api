using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Register()
    {
        IdentityUser user = new IdentityUser(){ UserName = "admin" };
        var result = await _userManager.CreateAsync(user, "Admin123!");
        var statusCode = result.Succeeded ? 200 : 400;

        return StatusCode(statusCode, result);
    }
}