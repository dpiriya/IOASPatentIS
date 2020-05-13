using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IOAS.Models.PatentIS
{
    public class IDFRequestVM
    {
        public long tranx_id { get; set; }
        [Required(ErrorMessage ="Error in Fetching FileNo") ]
        public long FileNo { get; set; }  
        public string IDFType { get; set; }
        public string DraftNo { get; set; }
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
        public string Commercialization { get; set; }
        public Nullable<bool> SourceOfInvention { get; set; }
        public string Disclosure { get; set; }
        public bool? BiologicalMaterial { get; set; }
        public string DetailsOfBiologicalMaterial { get; set; }
        public string RelevantInformation { get; set; }
        public string RequestedAction { get; set; }
        public string RequestedCRAction { get; set; }
        public string RequestedTMAction { get; set; }
        public string RequestedtxtAction { get; set; }
        public string RequestedCRtxtAction { get; set; }
        public string RequestedActionOthers { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public List<string> ListAction { get; set; }
        public List<string> TMListAction { get; set; }
        public List<string> CRListAction { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public bool formUpdate { get; set; }  
        public bool draftUpdate { get; set; }
        public string isDraft { get; set; }
        public List<CoInventorVM> CoIn { get; set; }   
        public List<ApplicantVM> Appl { get; set; }          
        public AnnexureB1VM Annex { get; set; }
        public TradeVM Trade { get; set; }
        public CopyRightVM CR { get; set; }
        public List<PatFilesVM> Files { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public System.Guid rowguid { get; set; }
        public IDFRequestVM()
        {
            CoIn = new List<CoInventorVM>();
            Appl = new List<ApplicantVM>();
            Annex = new AnnexureB1VM();
            Files = new List<PatFilesVM>();
            Trade = new TradeVM();
            CR = new CopyRightVM();
           
        }
    }
}