using MinimalEndpoints.Domain.Entities;

namespace MinimalEndpoints.Domain.Services.Interfaces;

public interface IRoleService
{
    Task<Role[]> GetAllAsync(CancellationToken ct);
}
