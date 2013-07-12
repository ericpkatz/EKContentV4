using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EKContent.bus.Abstract;
using EKContent.bus.Entitities;

namespace EKContent.web.Controllers
{
    public class FeedController : BaseEntityController<Feed, Feed>
    {
        public FeedController(IEKProvider dal) : base(dal, dal.FeedProvider)
        {
            
        }

    }
}