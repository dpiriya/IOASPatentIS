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
    
    public partial class tblBankProjectGroup
    {
        public int ProjectGroupId { get; set; }
        public Nullable<int> BankId { get; set; }
        public Nullable<int> BankAccountId { get; set; }
        public int ProjectGroup { get; set; }
        public Nullable<int> CrtdUserId { get; set; }
        public Nullable<System.DateTime> CrtdTS { get; set; }
        public Nullable<int> UptdUserId { get; set; }
        public Nullable<System.DateTime> UptdTS { get; set; }
        public Nullable<bool> Isdeleted { get; set; }
    }
}
