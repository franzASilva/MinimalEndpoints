using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MinimalEndpoints.Infrastructure.Data;

namespace MinimalEndpoints.API.Extensions;

public static class HealthChecksExtension
{
    public static IServiceCollection AddApiHealthChecks(this IServiceCollection services)
    {
        services
            .AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"])
            .AddCheck("test", () => HealthCheckResult.Unhealthy(), ["live"]) // test webhook notification
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

        services.AddHealthChecksUI(options =>
        {
            options.SetEvaluationTimeInSeconds(10);
            options.MaximumHistoryEntriesPerEndpoint(50);
            options.AddHealthCheckEndpoint("self", "/ready");
            options.AddWebhookNotification(
                name: "email",
                uri: "/api/v1.1/Email",
                payload: "{ \"message\": \"Report for [[LIVENESS]]: [[FAILURE]] - Description: [[DESCRIPTIONS]]\" }",
                restorePayload: "{ \"message\": \"[[LIVENESS]] is back to life\" }");
        }).AddInMemoryStorage();

        return services;
    }

    public static WebApplication DefineHealthCheckEndpoint(this WebApplication app)
    {
        app.UseHealthChecks(
            "/ready",
            new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse }
        );

        app.UseHealthChecks(
           "/live",
           new HealthCheckOptions { Predicate = r => r.Tags.Contains("live") }
        );

        app.UseHealthChecksUI(options =>
        {
            options.UIPath = "/healthz";
            options.AddCustomStylesheet("healthz.css");
        });

        return app;
    }
}
