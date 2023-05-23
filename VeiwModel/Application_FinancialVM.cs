using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebApplication20.Models;

namespace WebApplication20.VeiwModel
{
    public class ApplicantFilesViewModel
    {
        public string fileName { get; set; }
        public string fileBase { get; set; }
    }

    public class Application_FinancialVM
    {
        public App_Applicant App_Applicants { get; set; }

        public App_ExistingOwnership App_ExistingOwnerships { get; set; }

        public App_Financial App_Financials { get; set; }

        public App_AttachmentDocument App_AttachmentDocuments { get; set; }


        #region App_Applicant
        public long ApplicantID { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string IDNumber { get; set; }
        public string ResidentialAddress { get; set; }
        public string PostalAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public string FaxNumber { get; set; }
        [DisplayName("ApplicantEmail")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        public string Occupation { get; set; }
        public decimal IncomePerMonth { get; set; }
        public string NameOfEmployer { get; set; }
        public string SpouseSurname { get; set; }
        public string SpouseFirstname { get; set; }
        public string SpouseIDNumber { get; set; }
        public string SpouseResidentialAddress { get; set; }
        public string SpousePostalAddress { get; set; }
        public string SpouseTelephoneNumber { get; set; }
        public string SpouseFaxNumber { get; set; }
        [DisplayName("ApplicantSpouseEmail")]
        [DataType(DataType.EmailAddress)]
        public string SpouseEmailAddress { get; set; }
        public string SpouseOccupation { get; set; }
        public Nullable<decimal> SpouseIncomePerMonth { get; set; }
        public string SpouseNameOfEmployer { get; set; }
        public long PlotTypeID { get; set; }
        public long ErfSizeID { get; set; }
        public System.DateTime ApplicationDate { get; set; }
        public Nullable<bool> SignedByApplicant { get; set; }
        public Nullable<bool> SignedBySpouse { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] PHOTO { get; set; }
        public Nullable<bool> StageApplication { get; set; }
        public Nullable<bool> StageAllocation { get; set; }
        public Nullable<bool> StageSale { get; set; }
        public Nullable<bool> StageRegistration { get; set; }
        public Nullable<bool> StageValuation { get; set; }
        #endregion

        #region App_ExistingOwnership
        public long ExistingOwnershipID { get; set; }
        //public Nullable<long> ApplicantID { get; set; }
        public long ErfNumberID { get; set; }
        public string ErfSize { get; set; }
        public string ReferenceNumber { get; set; }
        public string Improvement { get; set; }
        public string LeaseAggreement { get; set; }
        public Nullable<decimal> OutstandingAmount { get; set; }
        public Nullable<System.DateTime> DateOfFirstOccupation { get; set; }
        public string WhoIsOccupyingTheErf { get; set; }
        public string WhoIsResponsibleForPayment { get; set; }
        //public string CreatedBy { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        //public string UpdatedBy { get; set; }
        //public Nullable<System.DateTime> UpdatedDate { get; set; }

        //public virtual App_Applicant App_Applicant { get; set; }
        //public virtual App_Erf App_Erf { get; set; }
        #endregion

        #region App_Financial
        public long FinancialID { get; set; }
        //public Nullable<long> ApplicantID { get; set; }
        public long PaymentMethodID { get; set; }
        public Nullable<decimal> EstimatedCostOfConstruction { get; set; }
        public Nullable<decimal> QualifiedLoanAmount { get; set; }
        public Nullable<System.DateTime> IntendedCommencementDate { get; set; }
        #endregion


        #region App_AttachmentDocument
        public long AttachmentDocumentID { get; set; }
        //public long ApplicantID { get; set; }
        public long AttachmentDocumentTypeID { get; set; }
        public byte[] AttachmentDocument { get; set; }
        public string AttachmentDocumentFileName { get; set; }
        #endregion


    }
}