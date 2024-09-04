using MinimalEndpoints.Domain.Entities;

namespace MinimalEndpoints.Domain.Repositories.Interfaces;

public interface IDummyRepository
{
    Task<Dummy[]> GetAllAsync(CancellationToken ct);
    Task<List<Dummy>> GetCompleteAsync(CancellationToken ct);
    Task<Dummy?> GetAsync(long id, CancellationToken ct);
    Task CreateAsync(Dummy dummy, CancellationToken ct);
    Task UpdateAsync(Dummy dummy, CancellationToken ct);
    Task<int?> DeleteAsync(long id, CancellationToken ct);
}
