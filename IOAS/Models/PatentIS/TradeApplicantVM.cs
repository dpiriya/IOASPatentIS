using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOAS.Models.PatentIS
{
    public class TradeApplicantVM
    {
        public long Sno { get; set; }
        public long FileNo { get; set; }
        public string Organisation { get; set; }
        public string Country { get; set; }
        public string Jurisdiction { get; set; }
        public string AddressOfService { get; set; }
        public string Nature { get; set; }
        public string LegalStatus { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}