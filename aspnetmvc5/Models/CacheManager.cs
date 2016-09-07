using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace aspnetmvc5.Models
{
    public class CacheManager
    {
        private static ObjectCache cache = MemoryCache.Default;

        public static void AddItem(string key,dynamic value)
        {
            cache.Add(key,value, DateTimeOffset.MaxValue);
        }

        public static dynamic GetItem(string key)
        {
            if (cache.Get(key) == null)
                return null;

           return cache.Get(key);
        }

        public static void RemoveItem(string key)
        {
            cache.Remove(key);
        }
    }
}