using Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class Notification : ExtendedModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string SubTitle { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string Body { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Data { get; set; }

        [Column(TypeName = "int")]
        public int Type { get; set; }
    }

    public class NotificationUsers
    {
        public int NotificationId { get; set; }

        public string UserId { get; set; }
    }
}
