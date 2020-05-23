using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Common
{
    public class EmailNotification
    {
        [Key]
        public int Id { get; set; }
        public string NotificationFrom  { get; set; }
        public string NotificationToEmail { get; set; }
        public string Subject { get; set; }
        public string EmailTemplate { get; set; }
        public int Type { get; set; }
        public int TypeId { get; set; }
    }
}
