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
        public string Token()
        {
            var now = DateTime.Now;
            var hours = now.Hour;
            var minutes = now.Minute - now.Minute % 5;
            return String.Format("{0}-{1}-{2}-{3}", Title, now.ToShortDateString(), hours, minutes);
        }
    }
}
