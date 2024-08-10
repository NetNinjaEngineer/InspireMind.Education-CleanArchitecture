using InspireMind.Education.Application.Contracts.Persistence;
using InspireMind.Education.Persistence.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InspireMind.Education.Persistence;

public static class PersistenceDependencies
{
    public static IServiceCollection AddPersistenceDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITopicRepository, TopicRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
