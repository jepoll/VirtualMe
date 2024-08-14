namespace WebApp.DTO;

public class JWTResponse
{
    public string? UserId { get; set; } = default!;
    public string Jwt { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    
}
