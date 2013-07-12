using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EKContent.bus.Concrete;
using EKContent.bus.Entities;
using EKContent.bus.Entitities;
using EKContent.bus.Services;

namespace EKContent.web.Models.ViewModels
{
    public class EditPageViewModel : BaseViewModel, IValidatableObject
    {
        public Page Page { get; set; }
        public int? ParentId { get; set; }
        public int? PageId { get; set; }
        public List<PageTypes> PageTypes()
        {
            return Enum.GetValues(typeof (PageTypes)).Cast<PageTypes>().ToList();
        }

        public List<SelectListItem> PageTypesSelectList()
        {
            return
                PageTypes().Select(
                    pt =>
                    new SelectListItem {Text = pt.ToString(), Value = pt.ToString(), Selected = Page.PageNavigation.PageType == pt}).
                    ToList();
        }

        private List<Feed> _feeds;
        public List<Feed> Feeds
        {
            set { _feeds = value; }
            get { return _feeds = _feeds ?? new List<Feed>(); }
        }

        public List<SelectListItem> SelectListFeeds()
        {
            var items =
                Feeds.Select(
                    i =>
                    new SelectListItem { Value = i.Id.ToString(), Text = i.Title.ToString(), Selected = Page.PageNavigation.FeedId == i.Id }).ToList();
            items.Insert(0, new SelectListItem { Text = "None", Value = "0" });
            return items;
        }

        public bool Inserting()
        {
            return ParentId.HasValue;
        }
        public int Id()
        {
            if (ParentId.HasValue)
                return ParentId.Value;
            else
                return Page.PageNavigation.Id;
        }

        public string TitleText()
        {
            var action =  Inserting() ? "Create" : "Update";
            return String.Format("{0} {1}", action, " Page");
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            var service = new PageService(new EKProvider());
            if ((this.Inserting()  || (!Inserting() && service.GetPage(this.Page.PageNavigation.Id).PageNavigation.PagePath().ToLower() != this.Page.PageNavigation.PagePath().ToLower())) && service.GetNavigation().Any(p => p.PagePath().ToLower() == this.Page.PageNavigation.PagePath().ToLower()))
                errors.Add(new ValidationResult("Page title is currently being used", new string[]{"Page.PageNavigation.Title"}));
            return errors;
        }
    }
}