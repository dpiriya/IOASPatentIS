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
    
    public partial class tblProjectTransactionlog
    {
        public int ProjectTransactionlogId { get; set; }
        public Nullable<System.DateTime> TranactionDate { get; set; }
        public string TransactionTypeCode { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> BudgetHeadId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Type { get; set; }
        public Nullable<bool> OBAdjustF { get; set; }
        public Nullable<int> CRTD_By { get; set; }
        public Nullable<System.DateTime> CRTD_TS { get; set; }
        public Nullable<int> RefId { get; set; }
    }
}
