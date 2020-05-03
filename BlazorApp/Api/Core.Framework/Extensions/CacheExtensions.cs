using Core.Shared.Cache;
using Core.Shared.Enums;
using Core.Shared.Response;
using System;

namespace Core.Framework.Extensions
{
    public static class CacheExtensions
    {
        public static TCacheItem GetCached<TCacheItem>(this ICacheProvider cacheProvider, string key, Func<TCacheItem> acquire, int cacheMinutes = 60, CacheExpiration cacheExpiration = CacheExpiration.SlidingExpiration)
        {
            var item = cacheProvider.Get<TCacheItem>(key);
            if (item != null)
                return item;

            var result = acquire();
            if (cacheMinutes > 0 && result != null)
                cacheProvider.Insert(key, result, cacheMinutes * 60, cacheExpiration);

            return result;
        }

        public static Response<TCacheItem> GetCachedWithCustomQuery<TCacheItem>(this ICacheProvider cacheProvider, string key, Func<Response<TCacheItem>> acquire, int cacheMinutes = 60, CacheExpiration cacheExpiration = CacheExpiration.SlidingExpiration)
        {
            var response = new Response<TCacheItem>();
            var item = cacheProvider.Get<TCacheItem>(key);
            if (item != null)
            {
                response.Payload = item;
                return response;
            }

            var acquireResponse = acquire();
            response.Merge(acquireResponse);

            if (response.NotOk)
                return response;

            if(cacheMinutes > 0)
                cacheProvider.Insert(key, acquireResponse.Payload, cacheMinutes * 60, cacheExpiration);

            response.Payload = acquireResponse.Payload;
            return response;
        }
    }
}
