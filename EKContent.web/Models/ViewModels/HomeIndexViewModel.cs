﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EKContent.bus.Entities;

namespace EKContent.web.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<PageNavigation> Pages { get; set; }
        public List<PageNavigation> TopLevelPages()
        {
            return Pages.Where(p =>  p.ParentId == HomePage().Id).ToList();
        }
        public PageNavigation HomePage()
        {
            return Pages.Single(p => p.IsHomePage());
        }

        public List<PageNavigation> ChildPages()
        {
            return Pages.Where(p => p.ParentId == Page.PageNavigation.Id).ToList();
        }

        public Site Site { get; set; }
        public Page Page { get; set; }
        public bool HasChildren()
        {
            return Pages.Any(p => p.ParentId == Page.PageNavigation.Id);
        }

        public bool ShowChildPages()
        {
            return ChildPages().Count > 0 && !Page.IsHomePage() && Page.PageType != PageTypes.Blog;
        }
        public bool CanDelete()
        {
            if (Page.IsHomePage())
                return false;
            if (HasChildren())
                return false;
            return true;
        }

        public bool ShowBreadCrumb()
        {
            return PagePathList().Count > 2;
        }

        public bool UserMode
        {
            get;
            set;
        }

        public bool EditMode()
        {
            return !UserMode;
        }


        private List<PageNavigation> _pagePathList = null;
        public List<PageNavigation> PagePathList()
        {
            if (_pagePathList != null)
                return _pagePathList;
            var page = Page.PageNavigation;
            var pages = new List<PageNavigation>();
            do
            {
                pages.Add(page);
                page = this.Pages.SingleOrDefault(p => p.Id == page.ParentId);
            } while (page != null);
            pages.Reverse();
            _pagePathList = pages;
            return _pagePathList;
        }
    }
}