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
    
    public partial class tblImprestRecoupBillBreakup
    {
        public int ImprestRecoupBillId { get; set; }
        public Nullable<int> ImprestPaymentDetailsId { get; set; }
        public Nullable<int> RecoupmentId { get; set; }
        public string ImprestRecoupBillNumber { get; set; }
        public string Particulars { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string VendorName { get; set; }
        public Nullable<System.DateTime> CRTD_TS { get; set; }
        public Nullable<int> CRTD_By { get; set; }
        public Nullable<System.DateTime> UPDT_TS { get; set; }
        public Nullable<int> UPDT_By { get; set; }
        public string Status { get; set; }
    }
}