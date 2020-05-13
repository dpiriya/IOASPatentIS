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
    
    public partial class tblBOAPaymentDetail
    {
        public int BOAPaymentDetailId { get; set; }
        public string ReferenceNumber { get; set; }
        public Nullable<System.DateTime> ReferenceDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string PayeeBank { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> Reconciliation_f { get; set; }
        public Nullable<int> BOAId { get; set; }
        public string TransactionType { get; set; }
        public Nullable<int> BankHeadID { get; set; }
        public string PayeeType { get; set; }
        public Nullable<int> PayeeId { get; set; }
        public string PayeeName { get; set; }
        public Nullable<int> PaymentMode { get; set; }
        public string TransactionID { get; set; }
        public string TransactionStatus { get; set; }
        public string ChequeNumber { get; set; }
        public string StudentRoll { get; set; }
        public string BankTransactionnumber { get; set; }
        public Nullable<System.DateTime> BankTransactionDate { get; set; }
        public string ChallanNo { get; set; }
    }
}
