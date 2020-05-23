using System;
using System.Collections.Generic;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class Pagination
    {
        public int PerPage { get; set; }
        public int PageNo { get; set; }
        public bool IncludeTotals { get; set; }

    }
}
