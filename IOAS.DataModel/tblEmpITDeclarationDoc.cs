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
    
    public partial class tblEmpITDeclarationDoc
    {
        public int DocumentId { get; set; }
        public Nullable<int> DeclarationID { get; set; }
        public Nullable<int> SectionID { get; set; }
        public string DocumentName { get; set; }
        public string Documentpath { get; set; }
        public string EmpNo { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
    }
}
