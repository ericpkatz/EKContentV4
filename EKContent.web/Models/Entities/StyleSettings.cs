using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ekUtilities;

namespace EKContent.web.Models.Entities
{
    public class StyleSettings : BaseEntity
    {
        public List<StyleSetting> Settings { get; set; }
    }
}
