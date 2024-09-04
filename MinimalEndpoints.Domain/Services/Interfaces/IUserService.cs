using MinimalEndpoints.Domain.Model;

namespace MinimalEndpoints.Domain.Services.Interfaces;

public interface IUserService
{
    Task<UserModel[]> GetAllAsync(CancellationToken ct);
    Task<UserModel?> GetAsync(string guid, CancellationToken ct);
    Task<UserModel?> CreateAsync(CreateUserModel createUserModel, CancellationToken ct);
    Task<UserModel?> UpdateAsync(UserModel userModel, CancellationToken ct);
    Task<int?> DeleteAsync(string guid, CancellationToken ct);
    Task<string?> Login(LoginUserModel userModel, CancellationToken ct);
}
