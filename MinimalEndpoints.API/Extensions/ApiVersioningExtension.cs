using Asp.Versioning;
using Asp.Versioning.Builder;

namespace MinimalEndpoints.API.Extensions;

public static class ApiVersioningExtension
{
    public static IServiceCollection AddApiVersion(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1.0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static RouteGroupBuilder MapApiVersion(this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1.0))
            .ReportApiVersions()
            .Build();

        return app
            .MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet);
    }
}
