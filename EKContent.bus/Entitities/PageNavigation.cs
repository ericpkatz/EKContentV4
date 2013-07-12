﻿using System;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using EKContent.bus.Entitities;
using ekUtilities;

namespace EKContent.bus.Entities
{


    public class PageNavigation : BaseEntity
    {
        public PageNavigation Clone()
        {
 
            return new PageNavigation
                       {
 
                           Id = 0,
                           ParentId = this.Id,
                           Title = this.Title,
                           Active = this.Active,
                           Description = this.Description,
                           ShowTitle = this.ShowTitle,
                           ShowTwitterFeed = this.ShowTwitterFeed
                       };
        }
        public int? ParentId { get; set; }
        public bool ShowTwitterFeed { get; set; }

        public bool ShowPageDescriptionInHeroUnit
        {
            get;
            set;
        }

        public int FeedId { get; set; }

        [ScriptIgnore]
        public Feed Feed { get; set; }

        public string PagePath()
        {
            var title = Title.Replace(' ', '_');
            return Regex.Replace(title, "[^A-Za-z0-9_]", String.Empty);
        }

        public bool ShowHeroUnit()
        {
            return this.ShowPageDescriptionInHeroUnit && !String.IsNullOrEmpty(Description);
        }

        public bool ShowSimpleDescription()
        {
            return !String.IsNullOrEmpty(Description) && !ShowPageDescriptionInHeroUnit;
        }

        public PageTypes PageType { get; set; }

        public bool IsHomePage()
        {
            return !ParentId.HasValue;
        }
        public DateTime DatePublished { get; set; }

        public void TransferTo(PageNavigation transferDataTo)
        {
            transferDataTo.Title = Title;
            transferDataTo.PageType = PageType;
            transferDataTo.Active = Active;
            transferDataTo.Description = Description;
            transferDataTo.Priority = Priority;
            transferDataTo.ShowTwitterFeed = ShowTwitterFeed;
            transferDataTo.ShowPageDescriptionInHeroUnit = ShowPageDescriptionInHeroUnit;
            transferDataTo.DatePublished = DatePublished;
            transferDataTo.FeedId = FeedId;
        }
    }
}