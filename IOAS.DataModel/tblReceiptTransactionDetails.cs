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
    
    public partial class tblReceiptTransactionDetails
    {
        public int PaymentModeId { get; set; }
        public Nullable<int> ReceiptId { get; set; }
        public Nullable<int> ModeofPayment { get; set; }
        public string TransactionNumber { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string TransactionBankName { get; set; }
        public Nullable<decimal> TransactionAmount { get; set; }
        public Nullable<int> TransactionType { get; set; }
        public Nullable<int> ForeignRemitCountry { get; set; }
        public Nullable<int> ForeignRemitCurrency { get; set; }
        public Nullable<decimal> ForeignCurrencyAmount { get; set; }
        public Nullable<decimal> ConversionRate { get; set; }
        public Nullable<System.DateTime> CrtdTS { get; set; }
        public Nullable<int> CrtdUserId { get; set; }
        public Nullable<System.DateTime> UpdtTS { get; set; }
        public Nullable<int> UpdtUserId { get; set; }
        public string Status { get; set; }
        public string TransactionBankBranch { get; set; }
        public Nullable<System.DateTime> TransactionClearanceDate { get; set; }
        public string DocPath { get; set; }
        public string DocName { get; set; }
    }
}
