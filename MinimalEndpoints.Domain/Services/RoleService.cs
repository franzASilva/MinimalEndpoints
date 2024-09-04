using MinimalEndpoints.Domain.Entities;
using MinimalEndpoints.Domain.Repositories.Interfaces;
using MinimalEndpoints.Domain.Services.Interfaces;

namespace MinimalEndpoints.Domain.Services;

public sealed class RoleService(IRoleRepository roleRepository) : IRoleService
{
    public async Task<Role[]> GetAllAsync(CancellationToken ct)
    {
        return await roleRepository.GetAllAsync(ct);
    }
}
