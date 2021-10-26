using Microsoft.Extensions.Caching.Memory;
using System;

namespace PandaTechEShop.Services.MemoryCacheProvider
{
    public class MemoryCacheProviderService : IMemoryCacheProviderService
    {
        private IMemoryCache _cache;

        public MemoryCacheProviderService()
        {
            _cache = new MemoryCache(new MemoryCacheOptions() { });
        }

        public void Set<T>(string key, T value, DateTimeOffset absoluteExpiry)
        {
            _cache.Set(key, value, absoluteExpiry);
        }

        public void Set<T>(string key, T value)
        {
            _cache.Set(key, value);
        }

        public T Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out T value))
            {
                return value;
            }
            else
            {
                return default(T);
            }
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Reset()
        {
            _cache = new MemoryCache(new MemoryCacheOptions() { });
        }
    }
}
