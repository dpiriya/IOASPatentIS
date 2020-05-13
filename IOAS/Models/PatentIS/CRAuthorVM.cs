using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOAS.Models.PatentIS
{
    public class CRAuthorVM
    {
        public long FileNo { get; set; }
        public int SNo { get; set; }
        public string AUName { get; set; }
        public string AUAddress { get; set; }
        public string AUNationality { get; set; }
        public Nullable<bool> isDeceased { get; set; }
        public Nullable<System.DateTime> deceasedDt { get; set; }
        public Nullable<System.DateTime> createdOn { get; set; }
        public string createdBy { get; set; }
    }
}