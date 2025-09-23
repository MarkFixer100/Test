using Domain.IReposotory;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


namespace Infostructure
{
    public class RedisCacheService : ICacheService
    {

        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GetAsync<T>(string key) where T : class
        {
            var cachedValue = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedValue))
            {
                return null;
            }

            return cachedValue;
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration) where T : class
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), options);
        }
    }
}
