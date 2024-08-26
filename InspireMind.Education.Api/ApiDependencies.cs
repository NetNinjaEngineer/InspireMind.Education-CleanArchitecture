using InspireMind.Education.Api.Middleware.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace InspireMind.Education.Api;

public static class ApiDependencies
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedMemoryCache();

        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("ar-EG"),
                new CultureInfo("en-US"),
                new CultureInfo("en")
            };

            options.SupportedCultures = supportedCultures;
            options.DefaultRequestCulture = new RequestCulture(supportedCultures[1]);

        });

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