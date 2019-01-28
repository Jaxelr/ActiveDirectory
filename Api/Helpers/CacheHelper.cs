using Api.Model.ActiveDirectory.Properties;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
using ServiceStack.ServiceHost;
using System;

namespace Api.Helpers
{
    public static class CacheHelper
    {
        public static object CachedContext<T>(this IRequestContext requestContext, ICacheClient cacheClient, string cacheKey, TimeSpan? expireCacheIn, Func<T> factoryFn)
            where T : class
        {
            string enable = Default.AppSettingKeys.CacheEnabled.ToString();
            if (ConfigUtils.GetAppSetting(enable, false))
            {
                var cachedResponse = cacheClient.Get<T>(cacheKey);

                if (cachedResponse != null)
                    return cachedResponse;

                if (expireCacheIn.HasValue)
                    cacheClient.Set(cacheKey, factoryFn(), expireCacheIn.Value);
                else
                    cacheClient.Set(cacheKey, factoryFn(), null);
            }
            return factoryFn();
        }
    }
}