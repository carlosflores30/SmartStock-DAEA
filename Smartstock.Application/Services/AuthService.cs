using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Smartstock.Application.Interfaces;
using Smartstock.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Smartstock.Application.Dtos;
using Smartstock.Application.Helpers;
using Smartstock.Domain.Entities;
using Smartstock.Domain.Utils;
using Smartstock.Application.Helpers;
using Microsoft.AspNetCore.Http;


namespace Smartstock.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration,  IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            Fullname = dto.Fullname,
            PasswordHash = PasswordHasher.Hash(dto.Password),
            Email = dto.Email,
            Role = dto.Role ?? "user",
            CreatedAt = DateTime.Now
        };

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveAsync();

        return new AuthResponseDto
        {
            Username = user.Username,
            Email = user.Email,
            Token = GenerateToken(user)
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var context = _httpContextAccessor.HttpContext!;
        var clientKey = LoginAttemptTracker.GetClientKey(context);
        if (LoginAttemptTracker.IsLocked(clientKey))
        {
            var remaining = LoginAttemptTracker.GetRemainingLockout(clientKey);
            throw new UnauthorizedAccessException($"Has superado los intentos. Intenta nuevamente en {remaining?.Minutes} minutos.");
        }
        var user = await _unitOfWork.UserRepository.GetByUsernameAsync(dto.Username);

        if (user == null || !PasswordHasher.Verify(dto.Password, user.PasswordHash))
        {
            LoginAttemptTracker.RegisterFailedAttempt(clientKey);
            int remaining = LoginAttemptTracker.GetRemainingAttempts(clientKey);

            if (remaining <= 0)
                throw new UnauthorizedAccessException("Has superado el número de intentos. Intenta nuevamente en 10 minutos.");
            else
                throw new UnauthorizedAccessException($"Credenciales inválidas. Te quedan {remaining} intento(s).");
        }
        LoginAttemptTracker.Reset(clientKey);
        return new AuthResponseDto
        {
            Username = user.Username,
            Email = user.Email,
            Token = GenerateToken(user)
        };
    }

    private string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpiresMinutes"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}