using System;
using System.Collections.Generic;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class ApprovalRequest
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int TypeId { get; set; }
        public string UserId { get; set; }
    }
}
