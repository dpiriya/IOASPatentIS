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
    
    public partial class tblCommitmentDetails
    {
        public int ComitmentDetailId { get; set; }
        public Nullable<int> CommitmentId { get; set; }
        public Nullable<int> AllocationHeadId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> BalanceAmount { get; set; }
        public Nullable<decimal> ReversedAmount { get; set; }
        public Nullable<decimal> ForignCurrencyValue { get; set; }
        public Nullable<decimal> ClosedAmount { get; set; }
    }
}
