using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }
        public string  AddedBy { get; set; }
        public int Type { get; set; }
        public int TypeId { get; set; }
        public DateTimeOffset AddedOn { get; set; }
    }
}
