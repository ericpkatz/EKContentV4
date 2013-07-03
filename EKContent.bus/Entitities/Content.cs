using System;
using System.Web.Script.Serialization;

namespace EKContent.bus.Entities
{
    public class Content : BaseContent
    {
        public string Body { get; set; }
        public int ImageId { get; set; }
        public int FileId { get; set; }
        public bool ShowAddThis { get; set; }

        public DateTime DatePublished { get; set; }

        [ScriptIgnore]
        public Image Image { get; set; }

        [ScriptIgnore]
        public EKFile File { get; set; }

        public bool BorderImage { get; set; }
    }
}