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
    
    public partial class tblTDSDocument
    {
        public int VendorIdentityDocumentId { get; set; }
        public Nullable<int> VendorId { get; set; }
        public string IdentityVendorDocument { get; set; }
        public string IdentityVendorAttachmentPath { get; set; }
        public string IdentityAttachmentName { get; set; }
        public Nullable<int> IdentityVendorDocumentType { get; set; }
        public Nullable<bool> IsCurrentVersion { get; set; }
        public Nullable<int> IdentityDocumentUploadUserId { get; set; }
        public Nullable<System.DateTime> IdentityDocumentUpload_Ts { get; set; }
    }
}
