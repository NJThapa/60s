using Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class UserDeviceToken : ExtendedModel
    {        
        [Required]
        public string UserId { get; set; }

        [Required]
        public string DeviceToken { get; set; }

        [Required]
        public int DeviceType { get; set; }
    }
}
