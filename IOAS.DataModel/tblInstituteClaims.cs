//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IOAS.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblInstituteClaims
    {
        public int InstituteClaimsId { get; set; }
        public Nullable<int> ClaimTypeId { get; set; }
        public string InstituteClaimsNumber { get; set; }
        public Nullable<System.DateTime> ClaimDate { get; set; }
        public Nullable<bool> ProjectFundReversal_f { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public string TravelBillNo { get; set; }
        public string PurchaseNo { get; set; }
        public string Others { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> ClaimAmount { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentActualName { get; set; }
        public string FacilityUsed { get; set; }
        public Nullable<int> BudgetHeadId { get; set; }
        public string Status { get; set; }
        public Nullable<int> UPDT_By { get; set; }
        public Nullable<System.DateTime> UPDT_TS { get; set; }
        public Nullable<int> CRTD_By { get; set; }
        public Nullable<System.DateTime> CRTD_TS { get; set; }
        public Nullable<int> ExpenseTypeId { get; set; }
        public Nullable<int> CommitmentId { get; set; }
        public Nullable<decimal> SpendAmount { get; set; }
    }
}
