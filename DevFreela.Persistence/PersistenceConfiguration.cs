using DevFreela.Domain.Interfaces;
using DevFreela.Persistence.Context;
using DevFreela.Persistence.Interceptors;
using DevFreela.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Persistence;

public static class PersistenceConfiguration
{
    public static IServiceCollection ConfigurePersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<SoftDeleteInterceptor>();
        services.AddSingleton<UpdateInterceptor>();

        var connectionString = configuration.GetConnectionString("DevFreelaCs");
        services.AddDbContext<DevFreelaDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString);
            options.EnableSensitiveDataLogging();
            options.AddInterceptors(
                sp.GetRequiredService<SoftDeleteInterceptor>(),
                sp.GetRequiredService<UpdateInterceptor>());
        });

        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        return services;
    }
}