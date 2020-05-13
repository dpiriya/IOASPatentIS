using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOAS.Models.PatentIS
{
    public class DraftCoInVM
    {
        public long tranx_id { get; set; }
        public long SNo { get; set; }
        public string FileNo { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Dept { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}