using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication20.Models;

namespace WebApplication20.VeiwModel
{
    public class App_AttachmentDocument
    {
        public long AttachmentDocumentID { get; set; }
        public long ApplicantID { get; set; }
        public long AttachmentDocumentTypeID { get; set; }
        public byte[] AttachmentDocument { get; set; }
        public string AttachmentDocumentFileName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual App_Applicant App_Applicant { get; set; }
        public virtual Sys_AttachmentDocumentType Sys_AttachmentDocumentType { get; set; }
    }
}