using Microsoft.Extensions.Options;
using MinimalEndpoints.API.Extensions;
using MinimalEndpoints.API.Swagger;
using MinimalEndpoints.Infrastructure.IoC;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddServices();
builder.Services.AddCors();
builder.Services.AddApiVersion();
builder.Services.AddEndpoints(typeof(Program).Assembly);
builder.Services.AddApiHealthChecks();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

var versionedGroup = app.MapApiVersion();
app.MapEndpoints(versionedGroup);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.DefineHealthCheckEndpoint();
app.Run();
