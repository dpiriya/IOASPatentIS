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
    
    public partial class tblImprestBillRecoupDetail
    {
        public int ImprestBillRecoupDetailId { get; set; }
        public Nullable<int> ImprestBillRecoupId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string TransactionType { get; set; }
        public Nullable<int> AccountGroupId { get; set; }
        public Nullable<int> AccountHeadId { get; set; }
    }
}
