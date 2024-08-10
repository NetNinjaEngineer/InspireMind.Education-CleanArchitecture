using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InspireMind.Education.Identity;

public static class IdentityDependencies
{
    public static IServiceCollection AddIdentityDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}
