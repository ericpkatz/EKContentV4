using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace ekUtilities
{
    public class WebCacheProvider : ICacheProvider
    {
        private Cache Store
        {
            get { return System.Web.HttpContext.Current.Cache; }
        }

        public void Set(string key, object value)
        {
            Store[key] = value;
        }

        public T Get<T>(string key)
        {
            var val = Store[key];
            if (val == null)
                return default(T);
            return (T)val;
        }

        public void Remove(string key)
        {
            Store.Remove(key);
        }
    }
}