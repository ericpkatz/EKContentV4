using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EKContent.web.Models.Database.Abstract;
using EKContent.web.Models.Entities;

namespace EKContent.web.Models.Database.Concrete
{
    public class CacheProvider : ICacheProvider
    {
        private System.Web.Caching.Cache Cache
        {
            get { return System.Web.HttpContext.Current.Cache; }
        }

        public List<Entities.CacheItem> Get()
        {
            var cache = System.Web.HttpContext.Current.Cache;
            List<CacheItem> cacheItems = new List<CacheItem>();
            var enumerator = cache.GetEnumerator();
            while(enumerator.MoveNext())
                cacheItems.Add(new CacheItem {Key = enumerator.Key.ToString()});
            return cacheItems;
           
        }

        public void Delete(Entities.CacheItem item)
        {
            Cache.Remove(item.Key);
        }


        public void Clear()
        {
            var enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext())
                Cache.Remove(enumerator.Key.ToString());
        }
    }
}