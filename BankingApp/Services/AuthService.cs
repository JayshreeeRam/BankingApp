using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly BankingContext _repo;
    private readonly IConfiguration _config;

    public AuthService(BankingContext context, IConfiguration config)
    {
        _repo = context;
        _config = config;
    }

    public AuthResponseDto Register(RegisterDto dto)
    {
        if (_repo.Users.Any(u => u.Username == dto.Username))
            throw new Exception("Username already exists");

        // 🔹 Password hash (replace with proper hashing in production)
        var user = new User
        {
            Username = dto.Username,
            Password = dto.Password,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            UserRole = dto.UserRole
        };

        _repo.Users.Add(user);
        _repo.SaveChanges();

        return new AuthResponseDto
        {
            Token = GenerateJwtToken(user),
            Username = user.Username,
            Role = user.UserRole.ToString()
        };
    }

    public AuthResponseDto Login(LoginDto dto)
    {
        var user = _repo.Users.FirstOrDefault(u => u.Username == dto.Username);
        if (user == null || user.Password != dto.Password)
            throw new UnauthorizedAccessException("Invalid credentials");

        return new AuthResponseDto
        {
            Token = GenerateJwtToken(user),
            Username = user.Username,
            Role = user.UserRole.ToString()
        };
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.UserRole.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
