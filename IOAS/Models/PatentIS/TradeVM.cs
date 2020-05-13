using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOAS.Models.PatentIS
{
    public class TradeVM
    {
        public long trx_id { get; set; }
        public long FileNo { get; set; }
        public string Category { get; set; }
        public List<string> Catlist { get; set; }
        public string TMName { get; set; }
        public string Description { get; set; }
        public bool TMImage { get; set; }
        public string Language { get; set; }
        public string Class { get; set; }
        public string TMStatement { get; set; }
        public List<TradeApplicantVM> TAppl { get; set; }
        public TradeVM()
        {
            TAppl = new List<TradeApplicantVM>();
        }
    }
}