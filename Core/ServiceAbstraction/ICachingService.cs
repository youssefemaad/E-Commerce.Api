namespace ServiceAbstraction;

public interface ICachingService
{
    Task<string?> GetAsync(string CacheKey);

    Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeTolive);
}
