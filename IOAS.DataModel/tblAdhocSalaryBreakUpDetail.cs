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
    
    public partial class tblAdhocSalaryBreakUpDetail
    {
        public int AdhocSalaryBreakUpDetailId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> HeadId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> PaymentId { get; set; }
        public Nullable<bool> IsTaxable { get; set; }
    }
}