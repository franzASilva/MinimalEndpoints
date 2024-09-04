using Microsoft.EntityFrameworkCore;
using MinimalEndpoints.Domain.Repositories.Interfaces;

namespace MinimalEndpoints.Infrastructure.Data.Repositories;

public sealed class Repository<TEntity>(MinimalEndpointsDbContext context) : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> dbSet = context.Set<TEntity>();

    public async Task<TEntity[]> GetAllAsync(CancellationToken ct)
    {
        return await dbSet.AsNoTracking().ToArrayAsync(ct);
    }

    public async Task<TEntity?> GetAsync(long id, CancellationToken ct)
    {
        return await dbSet.FindAsync([id, ct], cancellationToken: ct);
    }

    public async Task CreateAsync(TEntity entity, CancellationToken ct)
    {
        await dbSet.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken ct)
    {
        dbSet.Attach(entity);
        dbSet.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync(ct);
    }

    public async Task<int?> DeleteAsync(long id, CancellationToken ct)
    {
        if (await dbSet.FindAsync([id, ct], cancellationToken: ct) is TEntity entity)
        {
            dbSet.Remove(entity);
            return await context.SaveChangesAsync(ct);
        }

        return null;
    }

    public DbSet<TEntity> GetDbSet() => dbSet;
}
