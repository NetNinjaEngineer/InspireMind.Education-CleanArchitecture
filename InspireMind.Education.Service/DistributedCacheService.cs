using InspireMind.Education.Application.Contracts.Service;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace InspireMind.Education.Service;
public class DistributedCacheService(IDistributedCache cache) : IDistributedCacheService
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan expireTime)
    {
        if (response == null) return;

        var distributedCacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expireTime
        };

        var serializedResponse = JsonSerializer.Serialize(response, _serializerOptions);

        await cache.SetStringAsync(cacheKey, serializedResponse, distributedCacheEntryOptions);
    }

    public async Task<string?> GetCachedResponseAsync(string cacheKey)
    {
        var cacheResponse = await cache.GetStringAsync(cacheKey);
        if (cacheResponse == null) return null;
        return cacheResponse;
    }
}
