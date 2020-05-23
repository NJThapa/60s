using Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class Company : BaseModel
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        [Required]
        public string SubDomain { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
        public string BrandName { get; set; }
        public string Logo { get; set; }
        public string Terms { get; set; }

        [NotMapped]
        public List<ScriptField> ScriptFieldCollection { get; set; }

        public string ScriptFields
        {
            get
            {
                return ScriptFieldCollection != null ? JsonConvert.SerializeObject(ScriptFieldCollection) : null;
            }
            set
            {
                ScriptFieldCollection = !string.IsNullOrEmpty(value) ? JsonConvert.DeserializeObject<List<ScriptField>>(value) : null;
            }
        }
    }

    public class ScriptField
    {
        public int Index { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
