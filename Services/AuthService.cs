using TodoApp.Models;
using TodoApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace TodoApp.Services;

public class AuthService : IAuthService
{
    private readonly TodoDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(TodoDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public Task<User> GetUserAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task Register(RegisterViewModel request)
    {
        //HashAlgorithm hashAlgorithm = new SHA256Managed();

        var user = new User
        {
            Username = request.Username,
            Password = request.Password,
            Name = request.Name
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<string> Login(LoginViewModel request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.Username == request.Username && user.Password == request.Password);

        if (user == null)
        {
            throw new Exception("Invalid username or password");
        }

        // Return a Token
        return GenerateToken(user);
    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("username",user.Username),
            new Claim("userId", user.Id.ToString()),
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
