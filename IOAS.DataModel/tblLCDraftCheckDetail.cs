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
    
    public partial class tblLCDraftCheckDetail
    {
        public int LCDraftCheckDetailId { get; set; }
        public Nullable<int> LCDraftId { get; set; }
        public Nullable<int> Verified_By { get; set; }
        public Nullable<int> UPDT_By { get; set; }
        public Nullable<System.DateTime> CRTD_TS { get; set; }
        public Nullable<System.DateTime> UPDT_TS { get; set; }
        public Nullable<int> CRTD_By { get; set; }
        public Nullable<int> Delete_By { get; set; }
        public string Status { get; set; }
        public Nullable<int> FunctionCheckListId { get; set; }
    }
}