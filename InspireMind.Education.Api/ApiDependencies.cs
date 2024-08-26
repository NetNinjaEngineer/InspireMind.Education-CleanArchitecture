namespace InspireMind.Education.Api;

public static class ApiDependencies
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            var clientUrl = configuration.GetSection("ClientUrl").Value!;

            options.AddPolicy("CorsPolicy", options =>
                options.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(clientUrl));
        });

        return services;
    }
}