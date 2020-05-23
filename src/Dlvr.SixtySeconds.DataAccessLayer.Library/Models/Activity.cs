using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }
        public string ActorId { get; set; }
        public string ActorName { get; set; }
        public string ActivityType { get; set; }
        public string UserId { get; set; }

    }
}
