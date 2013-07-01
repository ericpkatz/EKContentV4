using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ekUtilities
{
    public interface ICacheProvider
    {
        void Set(string key, object value);
        T Get<T>(string key);
        void Remove(string key);
    }
}