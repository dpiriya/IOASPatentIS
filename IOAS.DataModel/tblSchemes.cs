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
    
    public partial class tblSchemes
    {
        public int SchemeId { get; set; }
        public string SchemeName { get; set; }
        public Nullable<int> ProjectType { get; set; }
        public string SchemeCode { get; set; }
        public string Status { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> Created_TS { get; set; }
        public Nullable<int> LastUpdatedUsedId { get; set; }
        public Nullable<System.DateTime> LastUpdated_TS { get; set; }
        public Nullable<decimal> OverheadPercentage { get; set; }
    }
}
