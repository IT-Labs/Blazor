using System;
using Core.Shared.Cache;

namespace Core.Framework.Cache
{
    public static class GiCache
    {

        public const int Minutes5 = 300;
        public const int Hour = 3600;
        #region Declarations

        private static ICacheProvider CacheProvider;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// 
        /// </summary>
        static GiCache()
        {
            CacheProvider = CacheProviderFactory.Get();
        }

        #endregion

        #region Methods

        public static TCacheItem Get<TCacheItem>(string key)
        {
            return CacheProvider.Get<TCacheItem>(key);
        }

        public static TCacheItem Get<TCacheItem>(string key, Func<TCacheItem> cacheLoader, int seconds = Minutes5)
        {
            return CacheProvider.Get(key, seconds, cacheLoader);
        }

        public static void Insert<TCacheItem>(string key, TCacheItem item, int seconds = Minutes5)
        {
            CacheProvider.Insert(key, item, seconds);
        }

        public static void Remove(string key, params string[] keys)
        {
            CacheProvider.Remove(key);
            if (keys != null)
            {
                foreach (var x in keys)
                {
                    CacheProvider.Remove(x);

                }
            }
        }


        #endregion
    }
}