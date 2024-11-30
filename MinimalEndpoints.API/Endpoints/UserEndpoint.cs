using Asp.Versioning;
using MinimalEndpoints.API.Endpoints.Interfaces;
using MinimalEndpoints.Domain.Model;
using MinimalEndpoints.Domain.Services.Interfaces;

namespace MinimalEndpoints.API.Endpoints;

public class UserEndpoint : IEndpoint
{
    private readonly string prefix = "User";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var userGroup = app
            .MapGroup($"/{prefix}")
            .WithTags($"{prefix} API")
            .HasApiVersion(new ApiVersion(1.1))
            .RequireAuthorization();

        userGroup.MapGet("/", GetAll)
            .WithSummary("Get all")
            .WithDescription("Get all users")
            .Produces<UserModel[]>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);

        userGroup.MapGet("/{guid}", Get)
            .WithSummary("Get user")
            .WithDescription("Get one user by guid")
            .WithName("GetUser")
            .Produces<UserModel>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);

        userGroup.MapPost("/", Create)
            .WithSummary("Create user")
            .WithDescription("Create new user")
            .Produces<UserModel>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        userGroup.MapPut("/", Update)
            .WithSummary("Update user")
            .WithDescription("Update existing user")
            .Produces<UserModel>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        userGroup.MapDelete("/{guid}", Delete)
            .WithSummary("Delete user")
            .WithDescription("Delete one user by guid")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound); ;
    }

    private async Task<IResult> GetAll(IUserService userService, CancellationToken ct) => 
        await userService.GetAllAsync(ct)
            is UserModel[] users
                ? TypedResults.Ok(users)
                : TypedResults.Problem(statusCode: StatusCodes.Status404NotFound);

    private async Task<IResult> Get(IUserService userService, string guid, CancellationToken ct) => 
        await userService.GetAsync(guid, ct)
            is UserModel user
                ? TypedResults.Ok(user)
                : TypedResults.Problem(statusCode: StatusCodes.Status404NotFound);

    private async Task<IResult> Create(IUserService userService, HttpContext context, CreateUserModel createUserModel, CancellationToken ct) => 
        await userService.CreateAsync(createUserModel, ct)
            is UserModel user
                ? TypedResults.CreatedAtRoute(routeName: "GetUser", routeValues: new { guid = user.Guid }, value: user)
                : TypedResults.Problem(statusCode: StatusCodes.Status400BadRequest);

    private async Task<IResult> Update(IUserService userService, UserModel userModel, CancellationToken ct) => 
        await userService.UpdateAsync(userModel, ct)
            is UserModel user
                ? TypedResults.Ok(user)
                : TypedResults.Problem(statusCode: StatusCodes.Status400BadRequest);

    private async Task<IResult> Delete(IUserService userService, string guid, CancellationToken ct) => 
        await userService.DeleteAsync(guid, ct)
            is (not null or > 0)
                ? TypedResults.Ok()
                : TypedResults.Problem(statusCode: StatusCodes.Status404NotFound);
}
