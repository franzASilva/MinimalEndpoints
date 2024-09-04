using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MinimalEndpoints.Domain.Settings;
using MinimalEndpoints.Infrastructure.Data;
using System.Text.Json;

namespace MinimalEndpoints.API.Extensions;

public static class HealthChecksExtension
{
    public static void AddApiHealthChecks(this IServiceCollection services)
    {
        services
            .AddHealthChecks()
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
    }

    public static void DefineHealthCheckEndpoint(this WebApplication app)
    {
        app.UseHealthChecks(
            "/healthcheck",
            new HealthCheckOptions { ResponseWriter = CustomResponseWriter }
        );
    }

    private static Task CustomResponseWriter(HttpContext context, HealthReport healthReport)
    {
        context.Response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(new
        {
            statusApplication = healthReport.Status.ToString(),
            healthChecks = healthReport.Entries.Select(e => new
            {
                check = e.Key,
                status = e.Value.Status.ToString(),
                errorMessage = e.Value.Exception?.Message,
                duration_ms = e.Value.Duration.Milliseconds,
                description = e.Value.Description
            })
        }, SerializerSettings.Default);

        return context.Response.WriteAsync(result);
    }
}
