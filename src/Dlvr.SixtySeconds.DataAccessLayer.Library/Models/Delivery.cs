using Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class Delivery : ExtendedModel
    {
        [Key]
        public int DeliveryId { get; set; }

        public int State { get; set; }
       
        [Column(TypeName = "varchar(200)")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string  VideoUrl{ get; set; }

        [ForeignKey("TaskItem")]
        public int TaskId { get; set; }
       
        [ForeignKey("Script")]
        public int ScriptId { get; set; }

        [ForeignKey("Scenario")]
        public int ScenarioId { get; set; }

    }
}
