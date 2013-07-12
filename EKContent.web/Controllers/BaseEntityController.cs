using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EKContent.bus.Abstract;
using EKContent.web.Models.ViewModels;
using ekUtilities;

namespace EKContent.web.Controllers
{
    public  class  BaseEntityController<T,B> : BaseController where T : BaseEntity where B : new()
    {
        private BaseService<T> EntityService { get; set; }
        public BaseEntityController(IEKProvider provider, BaseService<T> svc)
            : base(provider)
        {
            EntityService = svc;
        }

        List<T> Data
        {
            get { return EntityService.Get().ToList(); }
        }

        T Save(T t)
        {
            return EntityService.Set(t);
        }

        public ActionResult List(HomeIndexViewModel navigationModel, int id)
        {
            var model = new BaseEntityListViewModel<T>();
            model.Items = Data;
            model.NavigationModel = navigationModel;
            return View(String.Format("{0}List", typeof(T).Name), model);
        }

        public ActionResult Edit(HomeIndexViewModel navigationModel, int id, int? itemId)
        {
            var model = new BaseEntityEditViewModel<T>();
            T item = null;
            if (itemId.HasValue)
                item = EntityService.Get().SingleOrDefault(i => i.Id == itemId.Value);
            else
                item = (new B()) as T;
            model.NavigationModel = navigationModel;
            model.Item = item;
            return View(String.Format("{0}Edit", typeof(T).Name), model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(HomeIndexViewModel navigationModel, int id, BaseEntityEditViewModel<T> model)
        {
            model.NavigationModel = navigationModel;
            EntityService.Set(model.Item);

            TempData["message"] = "ItemSaved";
            return RedirectToAction("List", new { id = model.NavigationModel.Page.PageNavigation.Id });
        }

        //[HttpPost]
        public ActionResult Delete(HomeIndexViewModel navigationModel, int id, int itemId)
        {
            T item = EntityService.Get().SingleOrDefault(i => i.Id == itemId);
            EntityService.Delete(item);

            TempData["message"] = "Item Deleted";
            return RedirectToAction("List", new { id = id });
        }


    }
}
