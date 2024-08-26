using InspireMind.Education.Identity;
using InspireMind.Education.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InspireMind.Education.Api.Middleware;
public class MigrateDatabaseMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IServiceScopeFactory scopeFactory)
    {
        using var scope = scopeFactory.CreateScope();
        var database = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var identityDatabase = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
        await database.Database.MigrateAsync();
        await identityDatabase.Database.MigrateAsync();

        await next(context);
    }
}
