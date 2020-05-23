using Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class GalleryVideo : ExtendedModel
    {
        public Delivery Delivery { get; set; }
        public ScriptContent ScriptContent { get; set; }
        public Script Script { get; set; }
        public int PitchId { get; set; }
    }
}
