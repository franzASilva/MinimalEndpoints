namespace MinimalEndpoints.Domain.Entities;

public sealed class Dummy : Base
{
    public string Name { set; get; } = string.Empty;
    public bool IsComplete { set; get; }
}
