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
    
    public partial class tblHeadOpeningBalance
    {
        public int HeadOpeningBalanceId { get; set; }
        public Nullable<int> FinYearId { get; set; }
        public Nullable<int> AccountHeadId { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public Nullable<System.DateTime> CRTD_TS { get; set; }
        public Nullable<System.DateTime> UPTD_TS { get; set; }
        public Nullable<int> CRTD_By { get; set; }
        public Nullable<int> UPTD_By { get; set; }
        public string Status { get; set; }
        public string TransactionType { get; set; }
    }
}
