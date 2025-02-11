using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DevFreela.Application.Abstractions.Interfaces;
using DevFreela.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.Infra.Auth;

public class AuthService : IAuthService
{
    private readonly JwtOptions _jwtOptions;

    public AuthService(JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public string ComputeHash(string password)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

        var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

        var builder = new StringBuilder();

        foreach (var t in hashedBytes)
        {
            builder.Append(t.ToString("X"));
        }

        return hash;
    }

    public string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, string.Join(",", user.Roles.Select(r => r.ToString())))
        };

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}