using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EKContent.bus.Abstract;
using EKContent.bus.Services;
using EKContent.web.Models.ViewModels;
using System.IO;
using EKContent.bus.Entities;

namespace EKContent.web.Controllers
{
    [Authorize(Roles="Admin")]
    public class CacheController : BaseController
    {
        public CacheController(IEKProvider provider) : base(provider)
        {
        }

        public ActionResult List(HomeIndexViewModel homeIndexViewModel)
        {
            var items = _service.Dal.CacheProvider.Get().ToList();
            var model = new CacheListViewModel {Items = items, NavigationModel = homeIndexViewModel};
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(HomeIndexViewModel homeIndexViewModel, CacheItem cacheItem)
        {
            _service.Dal.CacheProvider.Delete(cacheItem);
            TempData["message"] = String.Format("Cache item of {0} has been removed.", cacheItem.Key);
            return RedirectToAction("List", new {pageId = homeIndexViewModel.Page.PageNavigation.Id});
        }

        [HttpPost]
        public ActionResult Clear(HomeIndexViewModel homeIndexViewModel)
        {
            _service.Dal.CacheProvider.Clear();
            TempData["message"] = String.Format("Cache has been cleared.");
            return RedirectToAction("List", new { pageId = homeIndexViewModel.Page.PageNavigation.Id });
        }


    }
}
