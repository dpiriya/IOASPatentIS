using System.Collections.Generic;

namespace IOAS.Models.PatentIS
{
    public class AnnexrptVM
    {
        public string BriefDescription { get; set; }
        public string DevelopmentStage { get; set; }
        public string OtherInfo { get; set; }
        public bool L1Search { get; set; }
        public string Tool { get; set; }
        public string Party { get; set; }
        public string Outcome { get; set; }
        public string Comments { get; set; }        
        public List<CommercialModeVM> Mode { get; set; }
        public List<ApplicationAreasVM> AppAreas { get; set; }
        public AnnexrptVM()
        {
            AppAreas = new List<ApplicationAreasVM>();
            Mode = new List<CommercialModeVM>();
        }
    }
}