using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ekUtilities;

namespace EKContent.bus.Entitities
{
    public class Feed : BaseEntity
    {
        public string URL { get; set; }
        public string Template { get; set; }
        public string SampleData { get; set; }
    }
}
