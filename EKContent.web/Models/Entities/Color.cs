using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ekUtilities;

namespace EKContent.web.Models.Entities
{
    public class Color : BaseEntity
    {
        public bool IsPalette { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}