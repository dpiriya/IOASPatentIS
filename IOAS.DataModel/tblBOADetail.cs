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
    
    public partial class tblBOADetail
    {
        public int BOADetailId { get; set; }
        public Nullable<int> BOAId { get; set; }
        public Nullable<int> CommitmentDetailId { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> BudgetHead { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<bool> Payment_f { get; set; }
    }
}
