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
    
    public partial class vwClosedCommitment
    {
        public string CommitmentNumber { get; set; }
        public Nullable<decimal> CommitmentAmount { get; set; }
        public string ProjectTitle { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> CRTD_By { get; set; }
        public string ClosedBy { get; set; }
        public string ProjectNumber { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public Nullable<decimal> WithdrawnAmount { get; set; }
    }
}
