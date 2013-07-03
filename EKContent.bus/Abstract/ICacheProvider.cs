using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EKContent.bus.Entities;

namespace EKContent.bus.Abstract
{
    public interface ICacheProvider
    {
        List<CacheItem> Get();
        void Delete(CacheItem item);
        void Clear();
    }
}