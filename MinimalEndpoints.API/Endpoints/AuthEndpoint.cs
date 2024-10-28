using Asp.Versioning;
using MinimalEndpoints.API.Endpoints.Interfaces;
using MinimalEndpoints.Domain.Model;
using MinimalEndpoints.Domain.Services.Interfaces;

namespace MinimalEndpoints.API.Endpoints;

public class AuthEndpoint : IEndpoint
{
    private readonly string prefix = "Auth";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var authGroup = app
            .MapGroup($"/{prefix}")
            .WithTags($"{prefix} API")
            .HasApiVersion(new ApiVersion(1.1))
            .AllowAnonymous();

        authGroup.MapPost("/", Login)
           .WithSummary("Log in")
           .WithDescription("Log user in")
           .Produces<string>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    private async Task<IResult> Login(IUserService userService, LoginUserModel loginUserModel, CancellationToken ct) => 
        await userService.Login(loginUserModel, ct)
            is string token
                ? TypedResults.Ok(token)
                : TypedResults.BadRequest();
}
