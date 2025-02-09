using DevFreela.Infra.Auth;
using DevFreela.Infra.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Infra;

public static class InfraConfiguration
{
    public static IServiceCollection ConfigureInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureAuth(configuration);
        services.ConfigurePersistence(configuration);

        return services;
    }
}