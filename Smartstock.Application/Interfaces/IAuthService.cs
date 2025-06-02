using Smartstock.Application.Dtos;

namespace Smartstock.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
}