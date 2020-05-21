using System;
using ActiveDirectory.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace ActiveDirectory
{
    public class Store
    {
        private readonly IMemoryCache cache;
        private readonly CacheConfig props;

        public Store(IMemoryCache cache, AppSettings appSettings) => (this.cache, props) = (cache, appSettings.Cache);

        /// <summary>
        /// Gets the cache value from store by key, if it misses, it sets the result as cached by the key provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The cache key to search on the store</param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public T GetOrSetCache<T>(string key, Func<T> fn) => GetOrSetCache(new string[] { key }, fn);

        /// <summary>
        /// Gets the cache value from store by the key array, if it misses, it sets the result as cached by the key provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The cache key to search on the store</param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public T GetOrSetCache<T>(string[] key, Func<T> fn)
        {
            var (res, cacheHit) = GetCache(key, fn);

            if (!cacheHit)
                SetCache(key, res);

            return res;
        }

        /// <summary>
        /// Set the store using the key provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The cache key to set the store value</param>
        /// <param name="res">The T value to store</param>
        public void SetCache<T>(string key, T res) => SetCache(new string[] { key }, res);

        /// <summary>
        /// Set the store using the key array provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The cache key to set the store value</param>
        /// <param name="res">The T value to store</param>
        public void SetCache<T>(string[] key, T res)
        {
            if (props.CacheEnabled)
            {
                string realKey = Key.Create<T>(key);

                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(props.CacheTimespan))
                    .SetSize(props.CacheMaxSize);

                cache.Set(realKey, res, options);
            }
        }

        /// <summary>
        /// Get the value on the store as based on the key, if missed return the result of invoking the Func<t>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public (T, bool) GetCache<T>(string key, Func<T> fn) => GetCache(new string[] { key }, fn);

        /// <summary>
        /// Get the value on the store as based on the key array, if missed return the result of invoking the Func<t>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public (T, bool) GetCache<T>(string[] key, Func<T> fn)
        {
            string realKey = Key.Create<T>(key);

            if (props.CacheEnabled && cache.TryGetValue(realKey, out T cachedRes))
            {
                return (cachedRes, true);
            }

            return (fn(), false);
        }
    }
}
