using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOAS.Models.PatentIS
{
    public class DraftApplicantVM
    {
        public long tranx_id { get; set; }
        public long Sno { get; set; }
        public string FileNo { get; set; }
        public string Organisation { get; set; }
        public string ContactName { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}