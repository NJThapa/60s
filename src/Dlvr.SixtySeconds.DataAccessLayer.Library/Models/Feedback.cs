using Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class Feedback : ExtendedModel
    {
        [Key]
        public int FeedbackId { get; set; }
        public int TypeId { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
    }
}
