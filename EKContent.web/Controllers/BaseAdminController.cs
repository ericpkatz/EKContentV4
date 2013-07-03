using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EKContent.bus.Abstract;
using EKContent.bus.Services;

namespace EKContent.web.Controllers
{
    public class BaseAdminController : BaseController
    {
        protected PageService _service;

        public BaseAdminController(IEKProvider provider) : base(provider)
        {
            _service = new PageService(provider);
        }
    }
}