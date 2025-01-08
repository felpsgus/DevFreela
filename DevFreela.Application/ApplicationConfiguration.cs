using System.Reflection;
using DevFreela.Application.Projects.Commands.InsertProject;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.AddMediatR();
        return services;
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}