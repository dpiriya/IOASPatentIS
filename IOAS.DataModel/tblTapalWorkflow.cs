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
    
    public partial class tblTapalWorkflow
    {
        public int TapalWorkflowId { get; set; }
        public Nullable<int> TapalId { get; set; }
        public Nullable<System.DateTime> DateTimeReceipt { get; set; }
        public Nullable<System.DateTime> InwardDateTime { get; set; }
        public Nullable<int> MarkTo { get; set; }
        public Nullable<int> Role { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> OutwardDateTime { get; set; }
        public Nullable<System.DateTime> CreatedTS { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<bool> Is_Active { get; set; }
        public Nullable<int> TapalAction { get; set; }
    }
}