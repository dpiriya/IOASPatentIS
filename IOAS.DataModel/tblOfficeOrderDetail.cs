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
    
    public partial class tblOfficeOrderDetail
    {
        public int OrderDetailId { get; set; }
        public Nullable<int> SalaryHeadId { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<decimal> RevisedSalary { get; set; }
        public Nullable<System.DateTime> ArrearFrom { get; set; }
        public Nullable<System.DateTime> ArrearPaymentDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsCurrent { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> OrderId { get; set; }
    }
}
