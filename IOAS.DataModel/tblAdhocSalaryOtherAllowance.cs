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
    
    public partial class tblAdhocSalaryOtherAllowance
    {
        public int AdhocSalaryOtherAllowanceId { get; set; }
        public Nullable<int> PaymentHeadId { get; set; }
        public Nullable<int> PaymentId { get; set; }
        public string EmployeeID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> EmpOtherAllowanceId { get; set; }
    }
}
