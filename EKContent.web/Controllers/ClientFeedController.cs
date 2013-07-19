using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EKContent.bus.Abstract;

namespace EKContent.web.Controllers
{
    public class ClientFeedController : BaseController
    {

        public ClientFeedController(IEKProvider dal) : base(dal)
        {
            //_service = new PageService(dal);
        }
        public string Index(int id)
        {
            var key = String.Format("Feed-{0}", id);
            var data = System.Web.HttpContext.Current.Cache[key] as string;
            if (data != null)
                return data;
            else
            {
                var feed = _service.Dal.FeedProvider.Get().SingleOrDefault(f => f.Id == id);
                var restClient = new RestSharp.RestClient(feed.URL);
                var request = new RestSharp.RestRequest();
                var response = restClient.Execute(request);
                data = String.Format("ExecuteFeed({0}, {1});", feed.Id, response.Content);
                System.Web.HttpContext.Current.Cache.Add(key, data, null, DateTime.Now.AddMinutes(5),System.Web.Caching.Cache.NoSlidingExpiration,
                                                         System.Web.Caching.CacheItemPriority.Low, null);
            }
            return data;
        }

    }
}
