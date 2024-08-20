using InspireMind.Education.Application.Contracts.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;

namespace InspireMind.Education.Application.Attributes;
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class DistributedCached : Attribute, IAsyncActionFilter
{
    private readonly int _expireTimeInSeconds;

    public DistributedCached(int expireTimeInSeconds)
    {
        _expireTimeInSeconds = expireTimeInSeconds;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger<DistributedCached>();

        var cacheService = context.HttpContext.RequestServices.GetRequiredService<IDistributedCacheService>();
        var CacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
        var cacheResponse = await cacheService.GetCachedResponseAsync(CacheKey);
        if (!string.IsNullOrEmpty(cacheResponse))
        {
            logger.LogInformation("Get Response From Cache.");
            var content = new ContentResult
            {
                Content = cacheResponse,
                StatusCode = 200,
                ContentType = "application/json"
            };

            context.Result = content;
            return;
        }

        var actionExecutedContext = await next.Invoke();
        if (actionExecutedContext.Result is OkObjectResult okObjectResult)
            await cacheService.CacheResponseAsync(
                cacheKey: CacheKey,
                response: okObjectResult.Value,
                expireTime: TimeSpan.FromSeconds(_expireTimeInSeconds)
                );

    }

    private static string GenerateCacheKeyFromRequest(HttpRequest request)
    {
        var cacheKeyBuilder = new StringBuilder();
        cacheKeyBuilder.Append(request.Path);
        foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            cacheKeyBuilder.Append($"|{key}-{value}");
        return cacheKeyBuilder.ToString();
    }
}
