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
    
    public partial class tblProjectStaffCategorywiseBreakup
    {
        public int ProjectStaffCategoryId { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> ProjectStaffCategory { get; set; }
        public Nullable<int> NoofStaffs { get; set; }
        public Nullable<decimal> SalaryofStaffs { get; set; }
        public Nullable<int> CrtdUserId { get; set; }
        public Nullable<System.DateTime> CrtdTS { get; set; }
        public Nullable<int> UpdtUserID { get; set; }
        public Nullable<System.DateTime> UpdtTS { get; set; }
    }
}