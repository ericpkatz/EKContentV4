using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ekUtilities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsNew()
        {
            return this.Id == 0;
        }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        private int _priority = 1;
        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }
        private bool _showTitle = true;
        public bool ShowTitle
        {
            get { return _showTitle; }
            set { _showTitle = value; }
        }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}