using DevFreela.Application;
using DevFreela.Persistence;

namespace DevFreela.Api;

public static class DependencyInjection
{
    public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigurePersistence(configuration);
        services.ConfigureApplication();
    }
}