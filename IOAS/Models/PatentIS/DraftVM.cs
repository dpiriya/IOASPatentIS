using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOAS.Models.PatentIS
{
    public class DraftVM
    {
        public long tranx_id { get; set; }
        public string DraftNo { get; set; }
        public string PrimaryInventorType { get; set; }
        public string PrimaryInventorName { get; set; }
        public string PIDepartment { get; set; }
        public string PIEmailId { get; set; }
        public string PIContactNo { get; set; }
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
        public Nullable<bool> SourceOfInvention { get; set; }
        public string Disclosure { get; set; }
        public Nullable<bool> BiologicalMaterial { get; set; }
        public string DetailsOfBiologicalMaterial { get; set; }
        public string RelevantInformation { get; set; }
        public string RequestedAction { get; set; }
        public string IDFType { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public bool isUpdate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public List<DraftCoInVM> CoIn { get; set; }
        public List<DraftApplicantVM> Appl { get; set; }
        public DraftAnnexVM Annex { get; set; }
        public List<PatFilesVM> Files { get; set; }
        public List<string> ListAction { get; set; }
        public DraftVM()
        {
            CoIn = new List<DraftCoInVM>();
            Appl = new List<DraftApplicantVM>();
            Annex = new DraftAnnexVM();
            Files = new List<PatFilesVM>();
        }
    }
}