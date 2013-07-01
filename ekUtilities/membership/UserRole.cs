using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ekUtilities.membership
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
