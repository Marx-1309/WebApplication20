using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication20.Models;

namespace WebApplication20.VeiwModel
{
    public class App_ExistingOwnership
    {


        public long ExistingOwnershipID { get; set; }
        public long ApplicantID { get; set; }
        public long ErfNumberID { get; set; }
        public string ErfSize { get; set; }
        public string ReferenceNumber { get; set; }
        public string Improvement { get; set; }
        public string LeaseAggreement { get; set; }
        public Nullable<decimal> OutstandingAmount { get; set; }
        public Nullable<System.DateTime> DateOfFirstOccupation { get; set; }
        public string WhoIsOccupyingTheErf { get; set; }
        public string WhoIsResponsibleForPayment { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [ForeignKey("ApplicantID")]
        public virtual App_Applicant App_Applicant { get; set; }
        public virtual App_Erf App_Erf { get; set; }
    }
}