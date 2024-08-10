using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InspireMind.Education.Infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
