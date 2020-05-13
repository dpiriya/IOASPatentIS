using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOAS.Models.PatentIS
{
    public class DraftcomModeVM
    {
        public long tranx_id { get; set; }
        public int Sno { get; set; }
        public long FileNo { get; set; }
        public int IndNo { get; set; }
        public string Category { get; set; }
        public string Mode { get; set; }
    }
}