using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOAS.Models.PatentIS
{
    public class ApplicantVM
    {       
        public long Sno { get; set; }
        public long FileNo { get; set; }
        public string Organisation { get; set; }
        public string ContactName { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }        

    }
}