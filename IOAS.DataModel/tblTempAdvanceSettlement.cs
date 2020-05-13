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
    
    public partial class tblTempAdvanceSettlement
    {
        public int TempAdvSettlementId { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> TemporaryAdvanceId { get; set; }
        public string TempSettlNumber { get; set; }
        public Nullable<int> ExpenseHead { get; set; }
        public Nullable<decimal> ExpenseAmount { get; set; }
        public string TransactionTypeCode { get; set; }
        public Nullable<decimal> TemporaryAdvanceValue { get; set; }
        public Nullable<decimal> TempSettlementValue { get; set; }
        public Nullable<decimal> BalanceinAdvance { get; set; }
        public Nullable<System.DateTime> CRTD_TS { get; set; }
        public Nullable<System.DateTime> UPTD_TS { get; set; }
        public Nullable<int> CRTD_By { get; set; }
        public Nullable<int> UPTD_By { get; set; }
        public string Status { get; set; }
        public Nullable<int> CheckListVerifiedBy { get; set; }
        public string SubCode { get; set; }
        public Nullable<decimal> PaymentValue { get; set; }
        public Nullable<bool> Pmt_f { get; set; }
    }
}
