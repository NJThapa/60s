using Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class Scenario : ExtendedModel
    {
        [Key]
        public int  ScenarioId { get; set; }
        public string  Title { get; set; }
        public int State { get; set; }
        public string Audience { get; set; }
        public string Situation { get; set; }
        public string Keywords { get; set; }
 

    }
}
