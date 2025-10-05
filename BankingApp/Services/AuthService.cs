using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly BankingContext _repo;
    private readonly IConfiguration _config;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(BankingContext context, IConfiguration config)
    {
        _repo = context;
        _config = config;
        _passwordHasher = new PasswordHasher<User>();
    }
    public AuthResponseDto Register(RegisterDto dto)
    {
        // Check for duplicate username
        if (_repo.Users.Any(u => u.Username == dto.Username))
            throw new Exception("Username already exists");

        // Check for duplicate email
        if (_repo.Users.Any(u => u.Email == dto.Email))
            throw new Exception("Email already exists");

        // Check for duplicate phone number
        if (_repo.Users.Any(u => u.PhoneNumber == dto.PhoneNumber))
            throw new Exception("Phone number already exists");

        var user = new User
        {
            Username = dto.Username,
            Password = dto.Password,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            UserRole = dto.UserRole
        };

        user.Password = _passwordHasher.HashPassword(user, dto.Password);

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
        try
        {
            var user = _repo.Users.FirstOrDefault(u => u.Username == dto.Username);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);

            if (result != PasswordVerificationResult.Success)
                throw new UnauthorizedAccessException("Invalid credentials");

            Console.WriteLine("User found:");
            Console.WriteLine($"Username: {user.Username}");
            Console.WriteLine($"UserRole: {user.UserRole}");
            Console.WriteLine($"UserId: {user.UserId}");

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Username = user.Username,
                Role = user.UserRole.ToString()
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("💥 Login failed:");
            Console.WriteLine(ex.ToString());
            throw; 
        }
    }


    //public AuthResponseDto Login(LoginDto dto)
    //{
    //    var user = _repo.Users.FirstOrDefault(u => u.Username == dto.Username);
    //    if (user == null || user.Password != dto.Password || user.UserRole!=dto.UserRole)
    //        throw new UnauthorizedAccessException("Invalid credentials");

    //        Console.WriteLine($"Login attempt: Username={dto.Username}, Role={dto.UserRole}");
    //    return new AuthResponseDto
    //    {
    //        Token = GenerateJwtToken(user),
    //        Username = user.Username,
    //        Role = user.UserRole.ToString()

    //    };
    //}



    //private string GenerateJwtToken(User user)
    //{
    //    var claims = new[]
    //    {
    //        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
    //        new Claim(ClaimTypes.Name, user.Username),
    //        new Claim(ClaimTypes.Role, user.UserRole.ToString())
    //    };

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //    var token = new JwtSecurityToken(
    //        issuer: _config["Jwt:Issuer"],
    //        audience: _config["Jwt:Audience"],
    //        claims: claims,
    //        expires: DateTime.UtcNow.AddHours(1),
    //        signingCredentials: creds
    //    );

    //    return new JwtSecurityTokenHandler().WriteToken(token);
    //}

    private string GenerateJwtToken(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString() ?? "0"),
        new Claim(ClaimTypes.Name, user.Username ?? "UnknownUser"),
        new Claim(ClaimTypes.Role, user.UserRole.ToString())

    };


        var keyString = _config["Jwt:Key"];
        if (string.IsNullOrEmpty(keyString))
            throw new InvalidOperationException("JWT Key is not configured.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
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
