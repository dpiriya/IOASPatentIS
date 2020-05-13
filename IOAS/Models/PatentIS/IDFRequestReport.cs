using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IOAS.Models.PatentIS
{
    public class IDFRequestReport
    {      
        [Required(ErrorMessage ="Error in Fetching FileNo") ]
        public long FileNo { get; set; }       
        public string IDFType { get; set; }
        public string PrimaryInventorType { get; set; }       
        public string PrimaryInventorName { get; set; }        
        public string PIDepartment { get; set; }
        public string PIEmailId { get; set; }
        public string PIContactNo { get; set; }
        public string PIInstId { get; set; }
        public string FirstApplicantName { get; set; }
        public string FirstApplicantOrganisation { get; set; }
        public string FirstApplicantPosition { get; set; }
        public string FirstApplicantAddress { get; set; }
        public string FirstApplicantEmailId { get; set; }
        public string FirstApplicantContactNo { get; set; }      
        public string Title { get; set; }
        public string FieldOfInvention { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string PriorPublication { get; set; }
        public string SupportInformation { get; set; }        
        public bool SourceOfInvention { get; set; }
        public string Disclosure { get; set; }
        public bool BiologicalMaterial { get; set; }
        public string DetailsOfBiologicalMaterial { get; set; }
        public string RelevantInformation { get; set; }
        public string RequestedAction { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public  DateTime CreatedOn { get; set; }
        public AnnexrptVM Annex { get; set; }
        public List<CoInventorVM> CoIn { get; set; }   
        public List<ApplicantVM> Appl { get; set; }
        public TraderptVM Trade { get; set; }
        public CopyRightrptVM CR { get; set; }
        public IDFRequestReport()
        {
            CoIn = new List<CoInventorVM>();
            Appl = new List<ApplicantVM>();
            Annex = new AnnexrptVM();
            Trade = new TraderptVM();
            CR = new CopyRightrptVM();
        }
    }
}