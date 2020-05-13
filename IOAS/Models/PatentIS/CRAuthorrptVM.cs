using System;

namespace IOAS.Models.PatentIS
{
    public class CRAuthorrptVM
    {
        public long FileNo { get; set; }
        public int SNo { get; set; }
        public string AUName { get; set; }
        public string AUAddress { get; set; }
        public string AUNationality { get; set; }
        public bool isDeceased { get; set; }
        public DateTime deceasedDt { get; set; }
    }
}