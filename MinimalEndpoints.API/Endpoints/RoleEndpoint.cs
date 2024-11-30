using Asp.Versioning;
using MinimalEndpoints.API.Endpoints.Interfaces;
using MinimalEndpoints.Domain.Entities;
using MinimalEndpoints.Domain.Services.Interfaces;

namespace MinimalEndpoints.API.Endpoints;

public class RoleEndpoint : IEndpoint
{
    private readonly string prefix = "Role";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var userGroup = app
            .MapGroup($"/{prefix}")
            .WithTags($"{prefix} API")
            .HasApiVersion(new ApiVersion(1.1))
            .AllowAnonymous();

        userGroup.MapGet("/", GetAll);
    }

    private async Task<IResult> GetAll(IRoleService roleService, CancellationToken ct) => 
        await roleService.GetAllAsync(ct)
            is Role[] roles
                ? TypedResults.Ok(roles)
                : TypedResults.NotFound();
}
