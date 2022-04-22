using Microsoft.AspNetCore.Identity;

namespace Domain.Auth.Models;

public class MApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
