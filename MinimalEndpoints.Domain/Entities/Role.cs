namespace MinimalEndpoints.Domain.Entities;

public partial class Role : Base
{
    public string Description { get; set; } = string.Empty;
    public bool Active { get; set; }

    public virtual ICollection<User>? Users { get; set; }
}
