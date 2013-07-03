using System;
using System.IO;
using ekUtilities;

namespace EKContent.bus.Entities
{
    public class EKFile : BaseEntity
    {
        public string FileName { get; set; }

        public string RelPath()
        {
            return String.Format("~/user_files/{0}", this.FileName);
        }

        public string AbsolutePath()
        {
            return System.Web.HttpContext.Current.Server.MapPath(RelPath());
        }

        public string NameWithExtension()
        {
            return String.Format("{0}{1}", Title, Path.GetExtension(FileName));
        }
    }
}