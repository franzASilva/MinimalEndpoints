using MinimalEndpoints.Domain.Entities;

namespace MinimalEndpoints.Domain.Services.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
