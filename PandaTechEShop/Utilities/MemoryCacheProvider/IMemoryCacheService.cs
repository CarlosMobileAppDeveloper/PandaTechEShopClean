using System;
namespace PandaTechEShop.Utilities.MemoryCacheProvider
{
    public interface IMemoryCacheProviderService
    {
        void Set<T>(string key, T value, DateTimeOffset absoluteExpiry);
        void Set<T>(string key, T value);
        T Get<T>(string key);
        void Remove(string key);
        void Reset();
    }
}
