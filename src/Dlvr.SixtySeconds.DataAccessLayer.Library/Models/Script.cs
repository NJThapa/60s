using Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class Script : ExtendedModel
    {
        [Key]
        public int ScriptId { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [ForeignKey("Scenario")]
        public int ScenarioId { get;set; }

        [Column(TypeName = "varchar(200)")]
        public string Title { get; set; }
        public int State { get; set; }

        public DateTimeOffset ApprovedOn { get; set; }

        public List<ScriptContent> ScriptContents { get; set; }
    }

    public class ScriptContent
    {
        [Key]
        public int ContentId { get; set; }
        
        [ForeignKey("Script")]
        public int ScriptId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
