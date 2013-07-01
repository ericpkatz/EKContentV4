using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EKContent.web.Models.Entities;

namespace EKContent.web.Models.Database.Abstract
{
    public interface ICacheProvider
    {
        List<CacheItem> Get();
        void Delete(CacheItem item);
        void Clear();
    }
}