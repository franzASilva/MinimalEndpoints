using MinimalEndpoints.Domain.Entities;

namespace MinimalEndpoints.Domain.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<Role[]> GetAllAsync(CancellationToken ct);
}
