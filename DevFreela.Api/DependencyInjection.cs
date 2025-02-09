using DevFreela.Application;
using DevFreela.Infra;

namespace DevFreela.Api;

public static class DependencyInjection
{
    public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureInfra(configuration);
        services.ConfigureApplication();
    }
}