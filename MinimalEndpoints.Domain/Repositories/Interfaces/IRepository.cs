using Microsoft.EntityFrameworkCore;

namespace MinimalEndpoints.Domain.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity[]> GetAllAsync(CancellationToken ct);
    Task<TEntity?> GetAsync(long id, CancellationToken ct);
    Task CreateAsync(TEntity entity, CancellationToken ct);
    Task UpdateAsync(TEntity entity, CancellationToken ct);
    Task<int?> DeleteAsync(long id, CancellationToken ct);
    DbSet<TEntity> GetDbSet();
}
