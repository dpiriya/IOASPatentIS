using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOAS.Models.PatentIS
{
    public class DesignClassVM
    {
        public long tranx_id { get; set; }
        public int VersionId { get; set; }
        public int Sno { get; set; }
        public long FileNo { get; set; }
        public int Index { get; set; }
        public int Class { get; set; }
        
        public List<SelectListItem> ClassList { get; set; }
    }
}