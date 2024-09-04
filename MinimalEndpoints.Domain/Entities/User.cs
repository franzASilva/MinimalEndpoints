namespace MinimalEndpoints.Domain.Entities;

public partial class User : Base
{
    public string Username { get; set; } = string.Empty;        
    public string PasswordHash { get; set; } = string.Empty;
    public string Guid { get; set; } = string.Empty;
    public bool Active { get; set; }

    public long? RoleId { get; set; }
    public virtual Role? Role { get; set; }
}
