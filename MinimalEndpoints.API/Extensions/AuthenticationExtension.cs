using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MinimalEndpoints.API.Extensions;

public static class AuthenticationExtension
{
    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Jwt:Key")!);

        services.AddAuthentication(a =>
        {
            a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(j =>
        {
            j.RequireHttpsMetadata = false;
            j.SaveToken = true;
            j.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"),
                ValidAudience = configuration.GetValue<string>("Jwt:Audience"),
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}
