using System.Linq;
using System.Web.Mvc;
using EKContent.bus.Abstract;
using EKContent.web.Models.ViewModels;
using EKContent.bus.Entities;

namespace EKContent.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ColorController : BaseController
    {

        public ColorController(IEKProvider dal) : base(dal)
        {
        }




        public ActionResult Edit(int id)
        {
            var model = new ColorEditViewModel { Colors = _service.Dal.ColorProvider.Get().ToList() };
            model.NavigationModel = HomeIndexViewModelLoader.Create(id, _service);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ColorEditViewModel model)
        {
            foreach (var color in _service.Dal.ColorProvider.Get())
                _service.Dal.ColorProvider.Delete(color);

            //_service.Dal.ColorProvider.Set(model.Colors);
            foreach(var color in model.Colors)
            {
                _service.Dal.ColorProvider.Set(color);
            }
            TempData["message"] = "Color settings have been set";
            return RedirectToAction("Edit", new {id = model.NavigationModel.Page.PageNavigation.Id});
        }

        [HttpPost]
        public ActionResult Add(ColorEditViewModel model)
        {
            ModelState.Clear();
            model.NavigationModel = HomeIndexViewModelLoader.Create(model.NavigationModel.Page.PageNavigation.Id, _service);
            model.Colors.Add(new Color {});
            return View("Edit", model);
        }
        [HttpPost]
        public ActionResult Remove(ColorEditViewModel model, int colorIdx)
        {
            ModelState.Clear();
            model.NavigationModel = HomeIndexViewModelLoader.Create(model.NavigationModel.Page.PageNavigation.Id, _service);
            model.Colors.RemoveAt(colorIdx);
            return View("Edit",  model);
        }

    }
}
