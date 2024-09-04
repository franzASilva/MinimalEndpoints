using Microsoft.OpenApi.Models;
using MinimalEndpoints.API.Swagger;

namespace MinimalEndpoints.API.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.OperationFilter<SwaggerDefaultValues>();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.\r\n\r\nEnter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerUI(this WebApplication app)
    {
        app.UseSwaggerUI(options =>
        {
            options.DefaultModelsExpandDepth(-1); // removes schemas
            var descriptions = app.DescribeApiVersions();

            foreach (var description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }
        });

        return app;
    }
}
