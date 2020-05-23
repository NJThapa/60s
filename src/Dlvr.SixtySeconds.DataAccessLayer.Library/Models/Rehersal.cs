using Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
   public class Rehersal: ExtendedModel
    {
        public int RehersalId { get; set; }
        public int TaskId { get; set; }
    }
}
