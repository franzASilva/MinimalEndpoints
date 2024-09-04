using MinimalEndpoints.Domain.Entities;

namespace MinimalEndpoints.Domain.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User[]> GetAllAsync(CancellationToken ct);
    Task<User?> GetAsync(string guid, CancellationToken ct);
    Task<User?> GetByUserNameAsync(string userName, CancellationToken ct);
    Task CreateAsync(User user, CancellationToken ct);
    Task UpdateAsync(User user, CancellationToken ct);
    Task<int?> DeleteAsync(string guid, CancellationToken ct);
}
