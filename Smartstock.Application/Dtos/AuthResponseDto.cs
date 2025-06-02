namespace Smartstock.Application.Dtos;

public class AuthResponseDto
{
    public string Token { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string? Email { get; set; }
}