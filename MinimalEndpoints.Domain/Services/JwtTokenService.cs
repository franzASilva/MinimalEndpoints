using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MinimalEndpoints.Domain.Entities;
using MinimalEndpoints.Domain.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalEndpoints.Domain.Services;

public sealed class JwtTokenService(IConfiguration configuration) : IJwtTokenService
{
    public string GenerateToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Jwt:Key")!);
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new(ClaimTypes.Sid, user.Guid),
                new(ClaimTypes.NameIdentifier, user.Username),
                new("roleId", user.Role?.Id.ToString() ?? "0"),
                new(ClaimTypes.Role, user.Role?.Description ?? string.Empty)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = configuration.GetValue<string>("Jwt:Issuer"),
            Audience = configuration.GetValue<string>("Jwt:Audience"),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
