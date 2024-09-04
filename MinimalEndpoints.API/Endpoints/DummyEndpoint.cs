using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using MinimalEndpoints.API.Endpoints.Interfaces;
using MinimalEndpoints.Domain.Constants;
using MinimalEndpoints.Domain.Model;
using MinimalEndpoints.Domain.Services.Interfaces;

namespace MinimalEndpoints.API.Endpoints;

public class DummyEndpoint : IEndpoint
{
    private readonly string prefix = "Dummy";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var dummyGroup = app
            .MapGroup($"/{prefix}")
            .WithTags($"{prefix} API")
            .RequireAuthorization(new AuthorizeAttribute() { Roles = $"{Roles.Admin},{Roles.User}" });

        dummyGroup.MapGet("/", GetAll).HasApiVersion(new ApiVersion(1.1));
        dummyGroup.MapGet("/complete", GetComplete).HasApiVersion(new ApiVersion(1.1));
        dummyGroup.MapGet("/{id}", Get).HasApiVersion(new ApiVersion(1.1));
        dummyGroup.MapGet("/", () => "Deprecated").HasDeprecatedApiVersion(new ApiVersion(1.0));
        dummyGroup.MapPost("/", Create).HasApiVersion(new ApiVersion(1.1));
        dummyGroup.MapPut("/", Update).HasApiVersion(new ApiVersion(1.1));
        dummyGroup.MapDelete("/{id}", Delete).HasApiVersion(new ApiVersion(2.0));
    }

    private async Task<IResult> GetAll(IDummyService dummyService, CancellationToken ct)
    {
        return await dummyService.GetAllAsync(ct) 
            is DummyModel[] dummies
                ? TypedResults.Ok(dummies)
                : TypedResults.NotFound();
    }

    private async Task<IResult> GetComplete(IDummyService dummyService, CancellationToken ct)
    {
        return await dummyService.GetCompleteAsync(ct)
            is List<DummyModel> dummies
                ? TypedResults.Ok(dummies)
                : TypedResults.NotFound();
    }

    private async Task<IResult> Get(IDummyService dummyService, long id, CancellationToken ct)
    {
        return await dummyService.GetAsync(id, ct)
            is DummyModel dummy
                ? TypedResults.Ok(dummy)
                : TypedResults.NotFound();
    }

    private async Task<IResult> Create(IDummyService dummyService, DummyModel dummyModel, CancellationToken ct)
    {
        return await dummyService.CreateAsync(dummyModel, ct)
            is DummyModel dummy
                ? TypedResults.Created($"{prefix}/{dummy.Id}", dummy)
                : TypedResults.BadRequest();
    }

    private async Task<IResult> Update(IDummyService dummyService, DummyModel dummyModel, CancellationToken ct)
    {
        return await dummyService.UpdateAsync(dummyModel, ct)
            is DummyModel dummy
                ? TypedResults.Ok(dummy)
                : TypedResults.BadRequest();
    }

    private async Task<IResult> Delete(IDummyService dummyService, int id, CancellationToken ct)
    {
        if (await dummyService.DeleteAsync(id, ct) is (not null or > 0))
        {
            return TypedResults.Ok();
        }

        return TypedResults.NotFound();
    }
}
