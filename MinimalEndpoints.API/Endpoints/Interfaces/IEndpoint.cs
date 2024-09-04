namespace MinimalEndpoints.API.Endpoints.Interfaces;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
