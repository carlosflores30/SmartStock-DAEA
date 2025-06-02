namespace Smartstock.Application.Dtos;

public class RegisterDto
{
    public string Username { get; set; } = null!;
    public string Fullname { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Email { get; set; }
    public string Role { get; set; } = "User";
}