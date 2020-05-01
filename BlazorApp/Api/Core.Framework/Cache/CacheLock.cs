using System.Collections.Generic;

namespace Core.Framework.Cache
{
    internal class CacheLock
    {

        private static readonly IDictionary<string, CacheLockItem> LockItems = new Dictionary<string, CacheLockItem>();
        private static readonly object ReadLock = new object();
        private static readonly object WriteLock = new object();



        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ContainsCacheItem(string key)
        {
            return LockItems.ContainsKey(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static CacheLockItem GetCacheItem(string key)
        {
            lock (ReadLock)
            {
                if (ContainsCacheItem(key))
                {
                    return LockItems[key];
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetLock(string key)
        {
            CacheLockItem item;

            lock (ReadLock)
            {
                if (ContainsCacheItem(key))
                {
                    item = LockItems[key];
                }
                else
                {
                    lock (WriteLock)
                    {
                        item = new CacheLockItem();
                        LockItems.Add(key, item);
                    }
                }
            }

            return item.Lock;
        }

    }
}