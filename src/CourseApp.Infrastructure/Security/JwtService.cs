using CourseApp.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourseApp.Infrastructure.Security;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAdminToken(int adminId, string code)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, adminId.ToString()),
            new Claim(ClaimTypes.Name, code),
            new Claim(ClaimTypes.Role, "Admin")
        };

        return BuildToken(claims);
    }

    public string GenerateTeacherToken(int teacherId, string codeNumber, string teacherType)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, teacherId.ToString()),
            new Claim(ClaimTypes.Name, codeNumber),
            new Claim(ClaimTypes.Role, "Teacher"),
            new Claim("teacher_type", teacherType)
        };

        return BuildToken(claims);
    }

    private string BuildToken(IEnumerable<Claim> claims)
    {
        var jwtKey = _configuration["Jwt:Key"];
        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException("JWT key is not configured.");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"] ?? "60")
            ),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}