using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication20.VeiwModel
{
    public class AppApplicant
    {
        public string ApplicantID { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string IDNumber { get; set; }
        public string ResidentialAddress { get; set; }
        public string PostalAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Occupation { get; set; }
        public string IncomePerMonth { get; set; }
        public string NameOfEmployer { get; set; }
        public string SignedByApplicant { get; set; }
        public string SpouseSurname { get; set; }
        public string SpouseFirstname { get; set; }
        public string SpouseIDNumber { get; set; }
        public string SpouseResidentialAddress { get; set; }
        public string SpousePostalAddress { get; set; }
        public string SpouseTelephoneNumber { get; set; }
        public string SpouseFaxNumber { get; set; }
        public string SpouseEmailAddress { get; set; }
        public string SpouseOccupation { get; set; }
        public string SpouseIncomePerMonth { get; set; }
        public string SpouseNameOfEmployer { get; set; }
        public string SignedBySpouse { get; set; }
        public string PlotTypeID { get; set; }
        public string ApplicationDate { get; set; }
        public string ErfSizeID { get; set; }
        public string ErfSize { get; set; }
    }

    public class AppExistingOwnership
    {
        public string ErfNumberID { get; set; }
        public string ReferenceNumber { get; set; }
        public string Improvement { get; set; }
        public string LeaseAggreement { get; set; }
        public string OutstandingAmount { get; set; }
        public string DateOfFirstOccupation { get; set; }
        public string WhoIsOccupyingTheErf { get; set; }
        public string WhoIsResponsibleForPayment { get; set; }
    }

    public class AppFinancials
    {
        public int PaymentMethodID { get; set; }
        public string EstimatedCostOfConstruction { get; set; }
        public string QualifiedLoanAmount { get; set; }
        public string IntendedCommencementDate { get; set; }
    }

    public class Root
    {
        public Root()
        {
            app_Applicant = new AppApplicant();
            app_ExistingOwnership = new AppExistingOwnership();
            app_Financials = new AppFinancials();

            Id_Doc_Files = new List<object>();
        }
        public AppApplicant app_Applicant { get; set; }
        public AppExistingOwnership app_ExistingOwnership { get; set; }
        public AppFinancials app_Financials { get; set; }
        public List<object> Id_Doc_Files { get; set; }
        public List<object> marriage_certificate_Files { get; set; }
        public List<object> id_doc_spouse_Files { get; set; }
        public List<object> comp_reg_certificate_Files { get; set; }
        public List<object> business_plan_Files { get; set; }
        public List<object> proof_of_funding_Files { get; set; }
    }
}