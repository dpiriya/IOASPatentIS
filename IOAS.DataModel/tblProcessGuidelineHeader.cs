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
    
    public partial class tblProcessGuidelineHeader
    {
        public int ProcessGuidelineId { get; set; }
        public string ProcessGuidelineTitle { get; set; }
        public string ProcessGuidelineDescription { get; set; }
        public Nullable<int> FunctionId { get; set; }
        public Nullable<System.DateTime> CreatedTS { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> LastUpdatedTS { get; set; }
        public Nullable<int> LastUpdatedUserId { get; set; }
    }
}
