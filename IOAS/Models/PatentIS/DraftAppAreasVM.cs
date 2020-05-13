using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOAS.Models.PatentIS
{
    public class DraftAppAreasVM
    {
        public long tranx_id { get; set; }
        public int Sno { get; set; }
        public long FileNo { get; set; }
        public int Index { get; set; }
        public string Category { get; set; }
        public string Areas { get; set; }
    }
}