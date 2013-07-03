using System.Collections.Generic;


namespace EKContent.bus.Entities
{
    public class Module : BaseContent
    {
        public ModuleTypes ModuleType { get; set; }

        private List<Content> _content = null;
        public List<Content> Content
        {
            set { _content = value; }
            get { return _content = _content ?? new List<Content>(); }
        }
    }
}