using MinimalEndpoints.Domain.Entities;
using MinimalEndpoints.Domain.Mappers;
using MinimalEndpoints.Domain.Model;
using MinimalEndpoints.Domain.Repositories.Interfaces;
using MinimalEndpoints.Domain.Services.Interfaces;

namespace MinimalEndpoints.Domain.Services;

public sealed class UserService(IUserRepository userRepository, IPasswordHashService passwordHashService, IJwtTokenService jwtTokenService) : IUserService
{
    public async Task<UserModel[]> GetAllAsync(CancellationToken ct)
    {
        var users = await userRepository.GetAllAsync(ct);

        if (users.Length != 0)
        {
            return users.Select(u => UserMapper.ToModel(u)).ToArray();
        }

        return [];
    }

    public async Task<UserModel?> GetAsync(string guid, CancellationToken ct)
    {
        if (await userRepository.GetAsync(guid, ct) is User user)
        {
            return UserMapper.ToModel(user);
        }

        return null;
    }

    public async Task<UserModel?> CreateAsync(CreateUserModel createUserModel, CancellationToken ct)
    {
        var user = UserMapper.ToEntity(createUserModel);
        user.PasswordHash = passwordHashService.HashSHA1(createUserModel.Password + user.Guid);
        await userRepository.CreateAsync(user, ct);
        return UserMapper.ToModel(user);
    }

    public async Task<UserModel?> UpdateAsync(UserModel userModel, CancellationToken ct)
    {
        if (await userRepository.GetAsync(userModel.Guid, ct) is User user)
        {
            user = UserMapper.ToEntity(userModel, user);
            await userRepository.UpdateAsync(user, ct);
            return UserMapper.ToModel(user);
        }

        return null;
    }

    public async Task<int?> DeleteAsync(string guid, CancellationToken ct) => await userRepository.DeleteAsync(guid, ct);

    public async Task<string?> Login(LoginUserModel userModel, CancellationToken ct)
    {
        if (await userRepository.GetByUserNameAsync(userModel.Username, ct) is User user
            && passwordHashService.VerifiesPassword(userModel.Password, user.PasswordHash, user.Guid))
        {
            return jwtTokenService.GenerateToken(user);
        }

        return null;
    }
}
