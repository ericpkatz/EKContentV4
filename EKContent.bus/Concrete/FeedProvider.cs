using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EKContent.bus.Entitities;
using ekUtilities;

namespace EKContent.bus.Concrete
{
    public class FeedProvider : IBaseService<Feed>
    {
        IBaseService<Feed> _svc = new BaseService<Feed>(new FileRepository<Feed>(), new WebCacheProvider(), null);
        public string Key
        {
            get { return _svc.Key; }
        }

        public ICollection<Feed> Get()
        {
            return _svc.Get();
        }

        public Feed Get(int? id)
        {
            return _svc.Get(id);
        }

        public Feed Set(Feed t)
        {
            var feed =  _svc.Set(t);
            new WebCacheProvider().Remove(String.Format("Feed-{0}", t.Id));
            return feed;

        }

        public void Clear()
        {
            _svc.Clear();
        }

        public void Delete(Feed t)
        {
            _svc.Delete(t);
            new WebCacheProvider().Remove(String.Format("Feed-{0}", t.Id));
        }
    }
}
