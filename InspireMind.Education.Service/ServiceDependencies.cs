using InspireMind.Education.Application.Contracts.Service;
using Microsoft.Extensions.DependencyInjection;

namespace InspireMind.Education.Service;
public static class ServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddScoped<IDistributedCacheService, DistributedCacheService>();
        return services;
    }
}