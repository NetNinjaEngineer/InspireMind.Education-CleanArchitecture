using Asp.Versioning;
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

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new MediaTypeApiVersionReader("ver")
                );
        })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
}