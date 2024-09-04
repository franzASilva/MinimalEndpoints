using Microsoft.Extensions.DependencyInjection;
using MinimalEndpoints.Domain.Repositories.Interfaces;
using MinimalEndpoints.Domain.Services;
using MinimalEndpoints.Domain.Services.Interfaces;
using MinimalEndpoints.Infrastructure.Data;
using MinimalEndpoints.Infrastructure.Data.Repositories;

namespace MinimalEndpoints.Infrastructure.IoC;

public static class Bootstrapper
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddDbContext<MinimalEndpointsDbContext>();        
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IDummyRepository, DummyRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddScoped<IDummyService, DummyService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPasswordHashService, PasswordHashService>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();
        

        return services;
    }
}
