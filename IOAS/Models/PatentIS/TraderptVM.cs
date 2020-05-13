using System.Collections.Generic;

namespace IOAS.Models.PatentIS
{
    public class TraderptVM
    {
        public long trx_id { get; set; }
        public long FileNo { get; set; }
        public string Category { get; set; }        
        public string TMName { get; set; }
        public string Description { get; set; }
        public bool TMImage { get; set; }
        public string Language { get; set; }
        public string Class { get; set; }
        public string TMStatement { get; set; }
        public List<TradeApplicantrptVM> TAppl { get; set; }
        public TraderptVM()
        {
            TAppl = new List<TradeApplicantrptVM>();
        }
    }
}