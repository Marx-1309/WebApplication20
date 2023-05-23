using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication20.Models;

namespace WebApplication20.VeiwModel
{
    public class ApplicantVM
    {
        //public AppApplicant app_Applicant { get; set; }
        //public AppExistingOwnership app_ExistingOwnership { get; set; }
        //public AppFinancials app_Financials { get; set; }
        //public List<object> Id_Doc_Files { get; set; }
        //public List<object> marriage_certificate_Files { get; set; }
        //public List<object> id_doc_spouse_Files { get; set; }
        //public List<object> comp_reg_certificate_Files { get; set; }
        //public List<object> business_plan_Files { get; set; }
        //public List<object> proof_of_funding_Files { get; set; }

        public App_Applicant app_Applicant { get; set; }
        public App_ExistingOwnership app_ExistingOwnership { get; set; }
        public App_Financial app_Financial { get; set; }
        //public App_AttachmentDocument app_Applicant { get; set; } 
        public ApplicantFilesViewModel[] Id_Doc_Files { get; set; }
        public ApplicantFilesViewModel[] marriage_certificate_Files { get; set; }
        public ApplicantFilesViewModel[] id_doc_spouse_Files { get; set; }
        public ApplicantFilesViewModel[] comp_reg_certificate_Files { get; set; }
        public ApplicantFilesViewModel[] business_plan_Files { get; set; }
        public ApplicantFilesViewModel[] proof_of_funding_Files { get; set; }
    }
}