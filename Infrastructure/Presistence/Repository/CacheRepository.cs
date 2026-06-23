using DomainLayer.Contracts;
using StackExchange.Redis;

namespace Presistence.Repository;

public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
{
    readonly IDatabase _database = connection.GetDatabase();
    public async Task<string?> GetAsync(string CacheKey)
    {
        var CasheValue = await _database.StringGetAsync(CacheKey);
        return CasheValue.IsNullOrEmpty ? null : CacheKey.ToString();
    }

    public async Task SetAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive)
    {
        await _database.StringSetAsync(CacheKey, CacheValue, TimeToLive);
    }

}
