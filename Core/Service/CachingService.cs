using System.Text.Json;
using DomainLayer.Contracts;
using ServiceAbstraction;

namespace Service;

class CachingService(ICacheRepository cacheRepository) : ICachingService
{
    public async Task<string?> GetAsync(string CacheKey) => await cacheRepository.GetAsync(CacheKey);

    public async Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeTolive)
    {
        var value = JsonSerializer.Serialize(CacheValue);
        await cacheRepository.SetAsync(CacheKey, value, TimeTolive);
    }

}
