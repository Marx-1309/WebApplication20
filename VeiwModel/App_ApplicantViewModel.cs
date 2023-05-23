using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication20.VeiwModel
{
    public class App_ApplicantViewModel
    {
        public App_ApplicantViewModel()
        {
        }

        public long ApplicantID { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string IDNumber { get; set; }
        public string ResidentialAddress { get; set; }
        public string PostalAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public string FaxNumber { get; set; }
        [DisplayName("ApplicantSpouseEmail")]
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
        [DisplayName("ApplicantEmail")]
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
    }
}