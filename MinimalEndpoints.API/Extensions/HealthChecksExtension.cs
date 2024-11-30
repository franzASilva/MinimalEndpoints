using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MinimalEndpoints.Domain.Model;
using MinimalEndpoints.Domain.Settings;
using MinimalEndpoints.Infrastructure.Data;
using System.Text.Json;

namespace MinimalEndpoints.API.Extensions;

public static class HealthChecksExtension
{
    public static IServiceCollection AddApiHealthChecks(this IServiceCollection services)
    {
        services
            .AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"])
            .AddDbContextCheck<MinimalEndpointsDbContext>();

        // others:
        //
        // SQL Server -AspNetCore.HealthChecks.SqlServer
        // Postgres - AspNetCore.HealthChecks.Npgsql
        // Redis - AspNetCore.HealthChecks.Redis
        // RabbitMQ - AspNetCore.HealthChecks.RabbitMQ
        // AWS S3 -AspNetCore.HealthChecks.Aws.S3
        // SignalR - AspNetCore.HealthChecks.SignalR
        // Uris - AspNetCore.HealthChecks.Uris

        return services;
    }

    public static WebApplication DefineHealthCheckEndpoint(this WebApplication app)
    {
        app.UseHealthChecks(
            "/health",
            new HealthCheckOptions { ResponseWriter = CustomResponseWriter }
        );

        app.UseHealthChecks(
           "/live",
           new HealthCheckOptions { Predicate = r => r.Tags.Contains("live") }
        );

        return app;
    }

    private static Task CustomResponseWriter(HttpContext context, HealthReport healthReport)
    {
        context.Response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(new HealthCheckModel
        (
            healthReport.Status.ToString(),
            healthReport.Entries.Select(e => new HealthCheckReportModel
            (
                e.Key,
                e.Value.Status.ToString(),
                e.Value.Exception?.Message,
                e.Value.Duration.Milliseconds,
                e.Value.Description
            ))
        ), SerializerSettings.Default);

        return context.Response.WriteAsync(result);
    }
}
