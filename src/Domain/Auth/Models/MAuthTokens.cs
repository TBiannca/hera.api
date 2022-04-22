namespace Domain.Auth.Models;

public class MAuthTokens
{
    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }
}