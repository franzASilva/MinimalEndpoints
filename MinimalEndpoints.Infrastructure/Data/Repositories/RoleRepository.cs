using MinimalEndpoints.Domain.Constants;
using MinimalEndpoints.Domain.Entities;
using MinimalEndpoints.Domain.Repositories.Interfaces;
using System.Reflection;

namespace MinimalEndpoints.Infrastructure.Data.Repositories;

public sealed class RoleRepository : IRoleRepository
{
    private readonly IRepository<Role> repository;

    public RoleRepository(IRepository<Role> repository)
    {
        this.repository = repository;

        // test purpose only: zzzZZZZZzzzz 🐢
        if (repository.GetDbSet().ToList().Count <= 0)
        {
            var ct = new CancellationToken();
            Type type = typeof(Roles);
            var flags = BindingFlags.Static | BindingFlags.Public;
            var fields = type.GetFields(flags);

            foreach (var field in fields)
            {
                repository.CreateAsync(new Role
                {
                    Active = true,
                    Description = field.Name,
                }, ct).Wait(ct);
            }
        }
    }

    public async Task<Role[]> GetAllAsync(CancellationToken ct) => await repository.GetAllAsync(ct);
}
