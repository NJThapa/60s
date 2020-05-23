using Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
   public class TaskItem : ExtendedModel
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public int State { get; set; }
        [ForeignKey("Script")]
        public int ScriptId { get; set; }
        
        [ForeignKey("Scenario")]
        public int ScenarioId { get; set; }
    }
}
