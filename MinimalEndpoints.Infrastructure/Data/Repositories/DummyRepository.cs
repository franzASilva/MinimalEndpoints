using Microsoft.EntityFrameworkCore;
using MinimalEndpoints.Domain.Entities;
using MinimalEndpoints.Domain.Repositories.Interfaces;

namespace MinimalEndpoints.Infrastructure.Data.Repositories;

public sealed class DummyRepository(IRepository<Dummy> repository) : IDummyRepository
{
    public async Task<Dummy[]> GetAllAsync(CancellationToken ct) => await repository.GetAllAsync(ct);

    public async Task<List<Dummy>> GetCompleteAsync(CancellationToken ct) => await repository.GetDbSet().Where(t => t.IsComplete).AsNoTracking().ToListAsync(ct);

    public async Task<Dummy?> GetAsync(long id, CancellationToken ct) => await repository.GetAsync(id, ct);

    public async Task CreateAsync(Dummy dummy, CancellationToken ct) => await repository.CreateAsync(dummy, ct);

    public async Task UpdateAsync(Dummy dummy, CancellationToken ct) => await repository.UpdateAsync(dummy, ct);

    public async Task<int?> DeleteAsync(long id, CancellationToken ct) => await repository.DeleteAsync(id, ct);    
}
