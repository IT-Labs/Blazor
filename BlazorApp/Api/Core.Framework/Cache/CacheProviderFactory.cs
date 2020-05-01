using BlazorApp.Shared.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Core.Framework.Cache
{
    public class CacheProviderFactory
    {
        public static ICacheProvider Get()
        {
            var cacheProvider = ContainerFactory.TryGetInstance<ICacheProvider>();

            if (cacheProvider == null)
            {
                var cache = ContainerFactory.TryGetInstance<IDistributedCache>();
                var logger = ContainerFactory.TryGetInstance<ILogger<RedisCacheProvider>>();
                cacheProvider = new RedisCacheProvider(cache, logger);
            }

            return cacheProvider;
        }
    }
}