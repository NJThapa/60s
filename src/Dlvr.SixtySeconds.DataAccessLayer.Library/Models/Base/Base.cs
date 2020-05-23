using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models.Base
{
    public class BaseModel
    {
        [Column(TypeName = "varchar(200)")]
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string UpdatedBy { get; set; }        
        public DateTimeOffset? UpdatedOn { get; set; }
        
        [Column(TypeName = "varchar(200)")]
        public string DeletedBy { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
    }

    public class ExtendedModel : BaseModel
    {
        public int CompanyId { get; set; }
    }
}
