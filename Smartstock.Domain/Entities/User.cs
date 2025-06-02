namespace Smartstock.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Fullname { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? Email { get; set; }
    public string Role { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}