using System;
using System.Collections.Generic;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class UserRole
    {
        public List<string> RoleNames { get; set; }
        public string UserId { get; set; }
    }
}
