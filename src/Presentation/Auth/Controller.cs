using Microsoft.AspNetCore.Mvc;

namespace Presentation.Auth;

[Route("auth")]
public class Controller : Microsoft.AspNetCore.Mvc.Controller
{
    [Route("register")]
    [HttpPost]
    public IActionResult Register()
    {
        return Ok();
    }
}