namespace MinimalEndpoints.Domain.Model;

public sealed record UserModel(string Username, long? RoleId, string RoleDescription, string Guid, bool Active);
