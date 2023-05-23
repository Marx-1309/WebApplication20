//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication20.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class App_Financial
    {
        public long FinancialID { get; set; }
        public long ApplicantID { get; set; }
        public long PaymentMethodID { get; set; }
        public Nullable<decimal> EstimatedCostOfConstruction { get; set; }
        public Nullable<decimal> QualifiedLoanAmount { get; set; }
        public Nullable<System.DateTime> IntendedCommencementDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual App_Applicant App_Applicant { get; set; }
        public virtual Sys_PaymentMethod Sys_PaymentMethod { get; set; }
    }
}
