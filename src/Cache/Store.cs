using System;
using Microsoft.Extensions.Caching.Memory;

namespace ActiveDirectory
{
    public class Store
    {
        private readonly IMemoryCache cache;
        private readonly CacheConfig props;

        public Store(IMemoryCache cache, CacheConfig props)
        {
            this.cache = cache;
            this.props = props;
        }

        public object GetOrSetCache<T>(string key, Func<T> fn)
        {
            string realKey = Key.Create<T>(key);

            if (props.CacheEnabled && cache.TryGetValue(realKey, out object cachedRes))
            {
                return cachedRes;
            }

            var res = fn();

            if (props.CacheEnabled)
            { 
                var options = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(props.CacheTimespan))
                    .SetSize(props.CacheMaxSize);

                cache.Set(realKey, res, options);
            }

            return res;
        }
    }
}
