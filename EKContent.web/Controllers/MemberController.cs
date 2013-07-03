using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EKContent.bus.Abstract;
using EKContent.bus.Entities;
using EKContent.web.Models.ViewModels;
using ekUtilities.membership;

namespace EKContent.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MemberController : BaseAdminController
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.Service = _service;
        }
        public MemberController(IEKProvider provider): base(provider)
        {

        }

        public ActionResult Index(HomeIndexViewModel model, int id)
        {
            List<EKRole> roles = new List<EKRole>();
            foreach (var role in Roles.GetAllRoles())
            {
                var r = new EKRole {Name = role};
                foreach (var member in Roles.GetUsersInRole(role))
                    r.Users.Add(new EKUser {UserName = member});
                roles.Add(r);
            }
            var memberIndexViewModel = new MemberIndexViewModel
                                           {
                                               NavigationModel = model,
                                               Roles = roles
                                           };
            return View(memberIndexViewModel);
        }
    }
}