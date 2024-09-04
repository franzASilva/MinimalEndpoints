using MinimalEndpoints.Domain.Entities;
using MinimalEndpoints.Domain.Model;

namespace MinimalEndpoints.Domain.Mappers;

public static class UserMapper
{
    public static User ToEntity(UserModel userModel, User user)
    {
        user.Active = userModel.Active;        
        user.RoleId = userModel.RoleId;
        return user;
    }

    public static User ToEntity(CreateUserModel createUserModel)
    {
        return new User
        {
            Active = true,
            Guid = Guid.NewGuid().ToString(),
            Username = createUserModel.Username
        };
    }

    public static UserModel ToModel(User user)
    {
        return new UserModel
        (
            user.Username,
            user.RoleId,
            user.Role?.Description ?? string.Empty,
            user.Guid,
            user.Active
        );
    }
}
