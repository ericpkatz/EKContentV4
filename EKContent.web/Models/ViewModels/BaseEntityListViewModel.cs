using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EKContent.bus.Entities;
using ekUtilities;

namespace EKContent.web.Models.ViewModels
{
    public class BaseEntityListViewModel<T> : BaseViewModel where T : BaseEntity
    {
        public List<T> Items { get; set; }
    }
}