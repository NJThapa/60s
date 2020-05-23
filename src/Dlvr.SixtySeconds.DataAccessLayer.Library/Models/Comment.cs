using Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class Comment:ExtendedModel
    {
        [Key]
        public int Id { get; set; }
        public int Type { get; set; }
        public int TypeId { get; set; }
        public string Content { get; set; }
    }
}
