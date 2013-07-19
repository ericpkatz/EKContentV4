using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ekUtilities;

namespace EKContent.web.Models.ViewModels
{
    public class BaseEntityWrapper
    {
        public BaseEntity Item { get; set; }
        public HomeIndexViewModel NavigationModel { get; set; }
    }
}