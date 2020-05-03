using System;
using System.Text;
using Core.Shared.Cache;
using Core.Shared.Enums;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Core.Framework.Cache
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<RedisCacheProvider> _logger;
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings();


        public RedisCacheProvider(IDistributedCache cache, ILogger<RedisCacheProvider> logger)
        {
            _cache = cache;
            _logger = logger;
        }


        public TCacheItem Get<TCacheItem>(string key, int seconds = 30, Func<TCacheItem> cacheLoader = null, CacheExpiration cacheExpiration = CacheExpiration.SlidingExpiration)
        {
            // Check the cache for the key.

            var item = GetItem<TCacheItem>(key);

            if (item == null)
            {
                // Get a lock on the key.

                lock (CacheLock.GetLock(key))
                {
                    item = GetItem<TCacheItem>(key);

                    if (item == null && cacheLoader != null)
                    {
                        // Get the data to insert into the cache.

                        item = cacheLoader();

                        if (item != null)
                        {
                            // Add data to the cache.

                            Insert(key, item, seconds, cacheExpiration);
                        }
                    }
                }
            }

            return item;
        }

        public void Insert<TCacheItem>(string key, TCacheItem item, int seconds = 30, CacheExpiration cacheExpiration = CacheExpiration.SlidingExpiration)
        {
            var json = JsonConvert.SerializeObject(item, _jsonSerializerSettings);
            var bytes = Encoding.ASCII.GetBytes(json);
            var distributedCacheEntryOptions = new DistributedCacheEntryOptions();
            var span = new TimeSpan(0, 0, seconds);
            switch (cacheExpiration)
            {
                case CacheExpiration.SlidingExpiration:
                    distributedCacheEntryOptions.SlidingExpiration = span;
                    break;
                case CacheExpiration.AbsoluteExpiration:
                    distributedCacheEntryOptions.AbsoluteExpiration = DateTime.UtcNow.Add(span);
                    break;
                case CacheExpiration.AbsoluteExpirationRelativeToNow:
                    distributedCacheEntryOptions.AbsoluteExpirationRelativeToNow = span;
                    break;
            }

            _cache.SetAsync(key, bytes, distributedCacheEntryOptions);
        }

        public void Remove(string key)
        {
            try
            {
                _cache.RemoveAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, " error while remove cache " + key);
            }
        }
        private TCacheItem GetItem<TCacheItem>(string key)
        {
            try
            {
                var value = _cache.GetAsync(key).Result;
                if (value != null)
                {
                    return JsonConvert.DeserializeObject<TCacheItem>(Encoding.ASCII.GetString(value), _jsonSerializerSettings);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(default(EventId), exception, exception.Message);

            }
            return default(TCacheItem);
        }
    }
}