using Microsoft.EntityFrameworkCore;
using MinimalEndpoints.Domain.Constants;
using MinimalEndpoints.Domain.Entities;
using MinimalEndpoints.Domain.Repositories.Interfaces;
using MinimalEndpoints.Domain.Services.Interfaces;

namespace MinimalEndpoints.Infrastructure.Data.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly IRepository<User> repository;

    public UserRepository(IRepository<User> repository, IRoleService roleService, IPasswordHashService passwordHashService)
    {
        this.repository = repository;

        // test purpose only
        if (repository.GetDbSet().FirstOrDefault(u => u.Username.Equals(RolesConst.Admin.ToLower())) is null)
        {
            var ct = new CancellationToken();
            var guid = Guid.NewGuid().ToString();
            var roleId = roleService.GetAllAsync(ct).Result?.FirstOrDefault(r => r.Description.Equals(RolesConst.Admin));

            repository.CreateAsync(new User
            {
                Active = true,
                Guid = guid,
                PasswordHash = passwordHashService.HashSHA1("123" + guid),
                RoleId = roleId?.Id,
                Username = RolesConst.Admin.ToLower()
            }, ct).Wait(ct);
        }
    }

    public async Task<User[]> GetAllAsync(CancellationToken ct) => await repository.GetAllAsync(ct);

    public async Task<User?> GetAsync(string guid, CancellationToken ct) => await repository.GetDbSet().FirstOrDefaultAsync(t => t.Guid.Equals(guid), ct);

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken ct) => await repository.GetDbSet().FirstOrDefaultAsync(t => t.Username.Equals(userName), ct);

    public async Task CreateAsync(User user, CancellationToken ct) => await repository.CreateAsync(user, ct);

    public async Task UpdateAsync(User user, CancellationToken ct) => await repository.UpdateAsync(user, ct);

    public async Task<int?> DeleteAsync(string guid, CancellationToken ct)
    {
        if (await repository.GetDbSet().FirstOrDefaultAsync(t => t.Guid.Equals(guid), ct) is User user)
        {
            return await repository.DeleteAsync(user.Id, ct);
        }

        return null;
    }    
}
