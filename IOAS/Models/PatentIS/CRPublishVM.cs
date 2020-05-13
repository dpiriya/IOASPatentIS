using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOAS.Models.PatentIS
{
    public class CRPublishVM
    {
        public Nullable<long> FileNo { get; set; }
        public Nullable<int> Sno { get; set; }
        public string PUName { get; set; }
        public string PUAddress { get; set; }
        public string PUNationality { get; set; }
        public Nullable<int> Year { get; set; }
        public string Country { get; set; }
    }
}