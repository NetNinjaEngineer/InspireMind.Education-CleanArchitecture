namespace InspireMind.Education.Application.Contracts.Service;
public interface IDistributedCacheService
{
    Task CacheResponseAsync(string cacheKey, object response, TimeSpan expireTime);

    Task<string?> GetCachedResponseAsync(string cacheKey);
}
