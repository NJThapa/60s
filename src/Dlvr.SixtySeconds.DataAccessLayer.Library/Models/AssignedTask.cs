using Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class AssignedTask : ExtendedModel
    {
        [Key]
        public int Id { get; set; }
        //Auth0 UserId
        //Auth0 UserId
        public string AssignedTo { get; set; }
        public DateTimeOffset AssignedOn { get; set; }
        public int State { get; set; }
    }
}
