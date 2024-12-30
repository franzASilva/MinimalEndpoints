using Asp.Versioning;
using MinimalEndpoints.API.Endpoints.Interfaces;

namespace MinimalEndpoints.API.Endpoints;

public class EmailEndpoint : IEndpoint
{
    private readonly string prefix = "Email";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var authGroup = app
            .MapGroup($"/{prefix}")
            .WithTags($"{prefix} API")
            .HasApiVersion(new ApiVersion(1.1))
            .AllowAnonymous();

        authGroup.MapPost("/", Healthz)
           .WithSummary("Healthz")
           .WithDescription("Health Check UI")
           .Produces<string>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    private IResult Healthz(ILogger<EmailEndpoint> logger, object message)
    {
        if (message is null)
        {
            return TypedResults.BadRequest();
        }

        logger.LogWarning("Healthz: {message}", message.ToString());
        return TypedResults.Ok(message);
    }
}
