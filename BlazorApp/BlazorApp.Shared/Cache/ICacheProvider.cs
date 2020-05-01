using System;
using BlazorApp.Shared.Enums;

namespace BlazorApp.Shared.Cache
{
    public interface ICacheProvider
    {
        /// <summary>
        /// Gets an item from the cache. If the item does not exist a function is provided to populate the item and store it in the cache for a specified time frame.
        /// </summary>
        /// <param name="key">The key for the item in cache to retrieve.</param>
        /// <param name="seconds">The time frame to store the item in cache.</param>
        /// <param name="cacheLoader">The function to load the time into cache.</param>
        /// <param name="cacheExpiration"></param>
        /// <returns></returns>
        TCacheItem Get<TCacheItem>(string key, int seconds = 30, Func<TCacheItem> cacheLoader = null, CacheExpiration cacheExpiration = CacheExpiration.SlidingExpiration);

        /// <summary>
        /// Inserts an item in the cache for a specified time frame.
        /// </summary>
        /// <param name="key">The key for the item in cache as.</param>
        /// <param name="item">The item to store in the cache.</param>
        /// <param name="seconds">The time frame to store the item in cache.</param>
        /// <param name="cacheExpiration"></param>
        void Insert<TCacheItem>(string key, TCacheItem item, int seconds = 30, CacheExpiration cacheExpiration = CacheExpiration.SlidingExpiration);

        /// <summary>
        /// Removes an item from the cache.
        /// </summary>
        /// <param name="key">The key for the item in cache to remove.</param>
        void Remove(string key);
        
    }
}