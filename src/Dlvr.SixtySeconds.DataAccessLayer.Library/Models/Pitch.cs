using Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class Pitch : ExtendedModel
    {
        [Key]
        public int PitchId { get; set; }

        [ForeignKey("Delivery")]
        public int DeliveryId { get; set; }
        public int State { get; set; }

        [ForeignKey("TaskItem")]
        public int TaskId { get; set; }

        [ForeignKey("Script")]
        public int ScriptId { get; set; }

        [ForeignKey("Scenario")]
        public int ScenarioId { get; set; }
        public string AddedBy { get; set; }
    }
}
