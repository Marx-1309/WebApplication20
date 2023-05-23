using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication20.Models;
using WebApplication20.VeiwModel;

namespace WebApplication20.Controllers
{
    public class App_ApplicantController : Controller
    {
        private PMISEntities db = new PMISEntities();

        // GET: App_Applicant
        public ActionResult Index()
        {
            var app_Applicant = db.App_Applicant;//.Include(a => a.Sys_ErfSize).Include(a => a.Sys_PlotType).OrderByDescending(x => x.CreatedDate).ToList();
            return View(app_Applicant);
        }

        // GET: App_Applicant/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_Applicant app_Applicant = db.App_Applicant.Find(id);
            if (app_Applicant == null)
            {
                return HttpNotFound();
            }
            return View(app_Applicant);
        }

        // GET: App_Applicant/Create
        public ActionResult Create()
        {
            ViewBag.ErfSizeID = new SelectList(db.Sys_ErfSize, "ErfSizeID", "ErfSize");
            ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType");

            //db.App_Applicant, "ApplicantID", "Surname", app_WaitingList.ApplicantID

            // ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname");

            ViewBag.PaymentMethodID = new SelectList(db.Sys_PaymentMethod, "PaymentMethodID", "PaymentMethod");


            ViewBag.ErfNumberID = new SelectList(db.App_Erf, "ErfNumberID", "ErfNumber");

            //ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname");

            //ViewBag.AttachmentDocumentTypeID = new SelectList(db.Sys_AttachmentDocumentType, "AttachmentDocumentTypeID", "AttachmentDocumentType");

            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Create(App_Applicant App_Applicants, App_ExistingOwnership app_ExistingOwnership,
                                    App_Financial app_financial, App_AttachmentDocument app_AttachmentDocuments,
                                     ApplicantFilesViewModel[] Id_Doc_Files, ApplicantFilesViewModel[] marriage_certificate_Files,
                                      ApplicantFilesViewModel[] id_doc_spouse_Files, ApplicantFilesViewModel[] comp_reg_certificate_Files,
                                       ApplicantFilesViewModel[] business_plan_Files, ApplicantFilesViewModel[] proof_of_funding_Files)
        {
            //Save file
            // var path = Server.MapPath("~/UploadedFiles/" + file.FileName);
            // file.SaveAs(path);
            if (string.IsNullOrEmpty(App_Applicants.Firstname) || string.IsNullOrEmpty(App_Applicants.Surname))
            {
                return View(App_Applicants);
            }
            string createdBy = User.Identity.Name ?? "admin";
            DateTime? createdDate = DateTime.Now;

            App_Applicants.CreatedBy = createdBy;
            App_Applicants.CreatedDate = createdDate;



            app_ExistingOwnership.CreatedBy = createdBy;
            app_ExistingOwnership.CreatedDate = createdDate;


            app_financial.CreatedBy = createdBy;
            app_financial.CreatedDate = createdDate;

            try
            {
                var getLatestapplicant = db.App_Applicant.Add(App_Applicants);
                await db.SaveChangesAsync();

                app_ExistingOwnership.ApplicantID = getLatestapplicant.ApplicantID;
                app_ExistingOwnership.ErfSize = App_Applicants.ErfSizeID.ToString();
                db.App_ExistingOwnership.Add(app_ExistingOwnership);
                await db.SaveChangesAsync();

                app_financial.ApplicantID = getLatestapplicant.ApplicantID;
                db.App_Financial.Add(app_financial);
                await db.SaveChangesAsync();


                //Save Id Docs into Database

                ICollection<App_AttachmentDocument> attachments = new List<App_AttachmentDocument>();

                // Id Documents
                if (Id_Doc_Files != null && Id_Doc_Files.Count() > 0)
                {
                    foreach (var docFile in Id_Doc_Files)
                    {
                        attachments.Add(new App_AttachmentDocument()
                        {
                            ApplicantID = getLatestapplicant.ApplicantID,
                            AttachmentDocumentFileName = docFile.fileName, //path,
                            AttachmentDocumentTypeID = 1, //app_AttachmentDocuments.AttachmentDocumentTypeID,
                            CreatedBy = User.Identity.Name ?? "admin",
                            AttachmentDocument = Convert.FromBase64String(docFile.fileBase.Split(',')[1]),  //System.IO.File.ReadAllBytes(path)
                            CreatedDate = DateTime.UtcNow
                        });
                    }
                }

                //Marriage Certificates
                if (marriage_certificate_Files != null && marriage_certificate_Files.Count() > 0)
                {
                    foreach (var docFile in marriage_certificate_Files)
                    {
                        attachments.Add(new App_AttachmentDocument()
                        {
                            ApplicantID = getLatestapplicant.ApplicantID,
                            AttachmentDocumentFileName = docFile.fileName,
                            AttachmentDocumentTypeID = 2,
                            CreatedBy = User.Identity.Name ?? "admin",
                            AttachmentDocument = Convert.FromBase64String(docFile.fileBase.Split(',')[1]),
                            CreatedDate = DateTime.UtcNow
                        });
                    }
                }
                // Spuose documents
                if (id_doc_spouse_Files != null && id_doc_spouse_Files.Count() > 0)
                {
                    foreach (var docFile in id_doc_spouse_Files)
                    {
                        attachments.Add(new App_AttachmentDocument()
                        {
                            ApplicantID = getLatestapplicant.ApplicantID,
                            AttachmentDocumentFileName = docFile.fileName,
                            AttachmentDocumentTypeID = 3,
                            CreatedBy = User.Identity.Name ?? "admin",
                            AttachmentDocument = Convert.FromBase64String(docFile.fileBase.Split(',')[1]),
                            CreatedDate = DateTime.UtcNow
                        });
                    }
                }

                // Company Reg. Files
                if (comp_reg_certificate_Files != null && comp_reg_certificate_Files.Count() > 0)
                {
                    foreach (var docFile in comp_reg_certificate_Files)
                    {
                        attachments.Add(new App_AttachmentDocument()
                        {
                            ApplicantID = getLatestapplicant.ApplicantID,
                            AttachmentDocumentFileName = docFile.fileName,
                            AttachmentDocumentTypeID = 6,
                            CreatedBy = User.Identity.Name ?? "admin",
                            AttachmentDocument = Convert.FromBase64String(docFile.fileBase.Split(',')[1]),
                            CreatedDate = DateTime.UtcNow
                        });
                    }
                }
                // Business Plan Files
                if (business_plan_Files != null && business_plan_Files.Count() > 0)
                {
                    foreach (var docFile in business_plan_Files)
                    {
                        attachments.Add(new App_AttachmentDocument()
                        {
                            ApplicantID = getLatestapplicant.ApplicantID,
                            AttachmentDocumentFileName = docFile.fileName,
                            AttachmentDocumentTypeID = 7,
                            CreatedBy = User.Identity.Name ?? "admin",
                            AttachmentDocument = Convert.FromBase64String(docFile.fileBase.Split(',')[1]),
                            CreatedDate = DateTime.UtcNow
                        });
                    }
                }


                // Proof of funding files
                if (proof_of_funding_Files != null && proof_of_funding_Files.Count() > 0)
                {
                    foreach (var docFile in proof_of_funding_Files)
                    {
                        attachments.Add(new App_AttachmentDocument()
                        {
                            ApplicantID = getLatestapplicant.ApplicantID,
                            AttachmentDocumentFileName = docFile.fileName,
                            AttachmentDocumentTypeID = 8,
                            CreatedBy = User.Identity.Name ?? "admin",
                            AttachmentDocument = Convert.FromBase64String(docFile.fileBase.Split(',')[1]),
                            CreatedDate = DateTime.UtcNow
                        });
                    }
                }

                db.App_AttachmentDocument.AddRange(attachments);
                await db.SaveChangesAsync();

            }
            catch (Exception exx)
            {
                exx.GetBaseException();
                return Json(new { status = false, message = "There is a problem while saving your record, please try again." });

            }
            return Json(new { status = true, message = "Record saved successfully" });


            #region OLD Code

            //App_Applicant app_Applicant = new App_Applicant
            //{
            //    //ApplicantID= application_FinancialVM.App_Applicants.ApplicantID,
            //    ApplicationDate = App_Applicants.ApplicationDate,
            //    CreatedBy = User.Identity.Name ?? "admin", //application_FinancialVM.App_Applicants.CreatedBy,
            //    CreatedDate = DateTime.Now, //application_FinancialVM.App_Applicants.CreatedDate,
            //    EmailAddress = App_Applicants.EmailAddress,
            //    ErfSizeID = App_Applicants.ErfSizeID,
            //    FaxNumber = App_Applicants.FaxNumber,
            //    Firstname = App_Applicants.Firstname,
            //    IDNumber = App_Applicants.IDNumber,
            //    IncomePerMonth = App_Applicants.IncomePerMonth,
            //    NameOfEmployer = App_Applicants.NameOfEmployer,
            //    Occupation = App_Applicants.Occupation,
            //    PHOTO = App_Applicants.PHOTO,
            //    PlotTypeID = App_Applicants.PlotTypeID,
            //    PostalAddress = App_Applicants.PostalAddress,
            //    ResidentialAddress = App_Applicants.ResidentialAddress,
            //    SignedByApplicant = App_Applicants.SignedByApplicant,
            //    SignedBySpouse = App_Applicants.SignedBySpouse,
            //    SpouseEmailAddress = App_Applicants.SpouseEmailAddress,
            //    SpouseFaxNumber = App_Applicants.SpouseFaxNumber,
            //    SpouseFirstname = App_Applicants.SpouseFirstname,
            //    SpouseIDNumber = App_Applicants.SpouseIDNumber,
            //    SpouseIncomePerMonth = App_Applicants.SpouseIncomePerMonth,
            //    SpouseNameOfEmployer = App_Applicants.SpouseNameOfEmployer,
            //    SpouseOccupation = App_Applicants.SpouseOccupation,
            //    SpousePostalAddress = App_Applicants.SpousePostalAddress,
            //    SpouseResidentialAddress = App_Applicants.SpouseResidentialAddress,
            //    SpouseSurname = App_Applicants.SpouseSurname,
            //    SpouseTelephoneNumber = App_Applicants.SpouseTelephoneNumber,
            //    StageAllocation = App_Applicants.StageAllocation,
            //    StageApplication = App_Applicants.StageApplication,
            //    StageRegistration = App_Applicants.StageRegistration,
            //    StageSale = App_Applicants.StageSale,
            //    StageValuation = App_Applicants.StageValuation,
            //    Surname = App_Applicants.Surname,
            //    Sys_ErfSize = App_Applicants.Sys_ErfSize,
            //    Sys_PlotType = App_Applicants.Sys_PlotType,
            //    TelephoneNumber = App_Applicants.TelephoneNumber,
            //    //UpdatedBy = App_Applicants.UpdatedBy,
            //    //UpdatedDate = App_Applicants.UpdatedDate,
            //};


            //App_ExistingOwnership app_Existing_Ownership = new App_ExistingOwnership()
            //{
            //    WhoIsResponsibleForPayment = app_ExistingOwnership.WhoIsResponsibleForPayment,
            //    App_Erf = app_ExistingOwnership.App_Erf,
            //    LeaseAggreement = app_ExistingOwnership.LeaseAggreement,
            //    OutstandingAmount = app_ExistingOwnership.OutstandingAmount,
            //    //ExistingOwnershipID = app_ExistingOwnership.ExistingOwnershipID,
            //    ApplicantID = app_ExistingOwnership.ApplicantID,
            //    App_Applicant = app_ExistingOwnership.App_Applicant,
            //    WhoIsOccupyingTheErf = app_ExistingOwnership.WhoIsOccupyingTheErf,
            //    DateOfFirstOccupation = app_ExistingOwnership.DateOfFirstOccupation,
            //    Improvement = app_ExistingOwnership.Improvement,
            //    ErfNumberID = app_ExistingOwnership.ErfNumberID,
            //    ErfSize = app_ExistingOwnership.ErfSize,
            //    ReferenceNumber = app_ExistingOwnership.ReferenceNumber,
            //    CreatedBy = User.Identity.Name ?? "admin", //app_ExistingOwnership.CreatedBy,
            //    CreatedDate = DateTime.Now, //app_ExistingOwnership.CreatedDate,
            //    //UpdatedBy = app_ExistingOwnership.UpdatedBy,
            //    //UpdatedDate = app_ExistingOwnership.UpdatedDate,

            //};

            //App_Financial app_Financial = new App_Financial
            //{
            //    ApplicantID = App_Financials.ApplicantID,
            //    CreatedBy = User.Identity.Name, //App_Financials.CreatedBy,
            //    CreatedDate = DateTime.Now, //App_Financials.CreatedDate,
            //    EstimatedCostOfConstruction = App_Financials.EstimatedCostOfConstruction,
            //    PaymentMethodID = App_Financials.PaymentMethodID,
            //    FinancialID = App_Financials.FinancialID,
            //    Sys_PaymentMethod = App_Financials.Sys_PaymentMethod,
            //    App_Applicant = App_Financials.App_Applicant,
            //    QualifiedLoanAmount = App_Financials.QualifiedLoanAmount,
            //    //UpdatedBy = App_Financials.UpdatedBy,
            //    IntendedCommencementDate = App_Financials.IntendedCommencementDate,
            //    //UpdatedDate = App_Financials.UpdatedDate,
            //};

            #endregion

        }

        //Remote Valindation
        public JsonResult IsApplicantIDAvailable(int ApplicantID)
        {
            return Json(!db.App_Applicant.Any(x => x.ApplicantID == ApplicantID),
                                                 JsonRequestBehavior.AllowGet);
        }




        // GET: App_Applicant/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_Applicant app_Applicant = db.App_Applicant.Find(id);
            if (app_Applicant == null)
            {
                return HttpNotFound();
            }

            // ViewBag.ErfSizeID = new SelectList(db.Sys_ErfSize, "ErfSizeID", "ErfSize");
            // ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType");

            var app_financial_result = db.App_Financial.FirstOrDefault(x => x.ApplicantID == app_Applicant.ApplicantID);
            var app_Existing_result = db.App_ExistingOwnership.FirstOrDefault(x => x.ApplicantID == app_Applicant.ApplicantID);
            var app_attachment_doc_result = db.App_AttachmentDocument.FirstOrDefault(x => x.ApplicantID == app_Applicant.ApplicantID);

            ViewBag.PaymentMethodID = new SelectList(db.Sys_PaymentMethod, "PaymentMethodID", "PaymentMethod", app_financial_result.PaymentMethodID);
            ViewBag.ErfNumberID = new SelectList(db.App_Erf, "ErfNumberID", "ErfNumber", app_Existing_result.ErfNumberID);
            ViewBag.ErfSizeID = new SelectList(db.Sys_ErfSize, "ErfSizeID", "ErfSize", app_Applicant.ErfSizeID);
            ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType", app_Applicant.PlotTypeID);


            Application_FinancialVM application_Financial = new Application_FinancialVM();

            application_Financial.App_Applicants = new App_Applicant();

            application_Financial.App_Applicants.ApplicantID = app_Applicant.ApplicantID;
            application_Financial.App_Applicants.ApplicationDate = app_Applicant.ApplicationDate;
            application_Financial.App_Applicants.App_ApplicantStatus = app_Applicant.App_ApplicantStatus;
            application_Financial.App_Applicants.CreatedBy = app_Applicant.CreatedBy;
            application_Financial.App_Applicants.CreatedDate = app_Applicant.CreatedDate;
            application_Financial.App_Applicants.EmailAddress = !string.IsNullOrEmpty(app_Applicant.EmailAddress) ? app_Applicant.EmailAddress.Trim() : app_Applicant.EmailAddress;
            application_Financial.App_Applicants.ErfSizeID = app_Applicant.ErfSizeID;
            application_Financial.App_Applicants.FaxNumber = !string.IsNullOrEmpty(app_Applicant.FaxNumber) ? app_Applicant.FaxNumber.Trim() : app_Applicant.FaxNumber;
            application_Financial.App_Applicants.Firstname = !string.IsNullOrEmpty(app_Applicant.Firstname) ? app_Applicant.Firstname.Trim() : app_Applicant.Firstname;
            application_Financial.App_Applicants.IDNumber = !string.IsNullOrEmpty(app_Applicant.IDNumber) ? app_Applicant.IDNumber.Trim() : app_Applicant.IDNumber;
            application_Financial.App_Applicants.IncomePerMonth = app_Applicant.IncomePerMonth;
            application_Financial.App_Applicants.NameOfEmployer = !string.IsNullOrEmpty(app_Applicant.NameOfEmployer) ? app_Applicant.NameOfEmployer.Trim() : app_Applicant.NameOfEmployer;
            application_Financial.App_Applicants.Occupation = !string.IsNullOrEmpty(app_Applicant.Occupation) ? app_Applicant.Occupation.Trim() : app_Applicant.Occupation;
            application_Financial.App_Applicants.PHOTO = app_Applicant.PHOTO;
            application_Financial.App_Applicants.PlotTypeID = app_Applicant.PlotTypeID;
            application_Financial.App_Applicants.PostalAddress = !string.IsNullOrEmpty(app_Applicant.PostalAddress) ? app_Applicant.PostalAddress.Trim() : app_Applicant.PostalAddress;
            application_Financial.App_Applicants.ResidentialAddress = !string.IsNullOrEmpty(app_Applicant.ResidentialAddress) ? app_Applicant.ResidentialAddress.Trim() : app_Applicant.ResidentialAddress;
            application_Financial.App_Applicants.SignedByApplicant = app_Applicant.SignedByApplicant;
            application_Financial.App_Applicants.SignedBySpouse = app_Applicant.SignedBySpouse;
            application_Financial.App_Applicants.SpouseEmailAddress = !string.IsNullOrEmpty(app_Applicant.SpouseEmailAddress) ? app_Applicant.SpouseEmailAddress.Trim() : app_Applicant.SpouseEmailAddress;
            application_Financial.App_Applicants.SpouseFaxNumber = !string.IsNullOrEmpty(app_Applicant.SpouseFaxNumber) ? app_Applicant.SpouseFaxNumber.Trim() : app_Applicant.SpouseFaxNumber;
            application_Financial.App_Applicants.SpouseFirstname = !string.IsNullOrEmpty(app_Applicant.SpouseFirstname) ? app_Applicant.SpouseFirstname.Trim() : app_Applicant.SpouseFirstname;
            application_Financial.App_Applicants.SpouseIDNumber = !string.IsNullOrEmpty(app_Applicant.SpouseIDNumber) ? app_Applicant.SpouseIDNumber.Trim() : app_Applicant.SpouseIDNumber;
            application_Financial.App_Applicants.SpouseIncomePerMonth = app_Applicant.SpouseIncomePerMonth;
            application_Financial.App_Applicants.SpouseNameOfEmployer = !string.IsNullOrEmpty(app_Applicant.SpouseNameOfEmployer) ? app_Applicant.SpouseNameOfEmployer.Trim() : app_Applicant.SpouseNameOfEmployer;
            application_Financial.App_Applicants.SpouseOccupation = !string.IsNullOrEmpty(app_Applicant.SpouseOccupation) ? app_Applicant.SpouseOccupation.Trim() : app_Applicant.SpouseOccupation;
            application_Financial.App_Applicants.SpousePostalAddress = !string.IsNullOrEmpty(app_Applicant.SpousePostalAddress) ? app_Applicant.SpousePostalAddress.Trim() : app_Applicant.SpousePostalAddress;
            application_Financial.App_Applicants.SpouseResidentialAddress = app_Applicant.SpouseResidentialAddress;
            application_Financial.App_Applicants.SpouseSurname = !string.IsNullOrEmpty(app_Applicant.SpouseSurname) ? app_Applicant.SpouseSurname.Trim() : app_Applicant.SpouseSurname;
            application_Financial.App_Applicants.SpouseTelephoneNumber = !string.IsNullOrEmpty(app_Applicant.SpouseTelephoneNumber) ? app_Applicant.SpouseTelephoneNumber.Trim() : app_Applicant.SpouseTelephoneNumber;
            application_Financial.App_Applicants.StageAllocation = app_Applicant.StageAllocation;
            application_Financial.App_Applicants.StageApplication = app_Applicant.StageApplication;
            application_Financial.App_Applicants.StageRegistration = app_Applicant.StageRegistration;
            application_Financial.App_Applicants.StageSale = app_Applicant.StageSale;
            application_Financial.App_Applicants.Surname = !string.IsNullOrEmpty(app_Applicant.Surname) ? app_Applicant.Surname.Trim() : app_Applicant.Surname;
            application_Financial.App_Applicants.StageValuation = app_Applicant.StageValuation;
            application_Financial.App_Applicants.TelephoneNumber = !string.IsNullOrEmpty(app_Applicant.TelephoneNumber) ? app_Applicant.TelephoneNumber.Trim() : app_Applicant.TelephoneNumber;

            //   UpdatedBy = string.IsNullOrEmpty(User.Identity.Name) ? "admin" : User.Identity.Name,
            // UpdatedDate=DateTime.UtcNow,

            application_Financial.App_Financials = new App_Financial();

            //UpdatedBy = string.IsNullOrEmpty(User.Identity.Name) ? "admin" : User.Identity.Name,
            //UpdatedDate = DateTime.UtcNow,
            application_Financial.App_Financials.ApplicantID = app_Applicant.ApplicantID;
            application_Financial.App_Financials.EstimatedCostOfConstruction = app_financial_result.EstimatedCostOfConstruction;
            application_Financial.App_Financials.FinancialID = app_financial_result.FinancialID;
            application_Financial.App_Financials.IntendedCommencementDate = app_financial_result.IntendedCommencementDate;
            application_Financial.App_Financials.PaymentMethodID = app_financial_result.PaymentMethodID;
            application_Financial.App_Financials.QualifiedLoanAmount = app_financial_result.QualifiedLoanAmount;

            application_Financial.App_ExistingOwnerships = new App_ExistingOwnership();
            application_Financial.App_ExistingOwnerships.ApplicantID = app_Existing_result.ApplicantID;
            application_Financial.App_ExistingOwnerships.DateOfFirstOccupation = app_Existing_result.DateOfFirstOccupation;
            application_Financial.App_ExistingOwnerships.ErfNumberID = app_Existing_result.ErfNumberID;
            application_Financial.App_ExistingOwnerships.ExistingOwnershipID = app_Existing_result.ExistingOwnershipID;
            application_Financial.App_ExistingOwnerships.Improvement = !string.IsNullOrEmpty(app_Existing_result.Improvement) ? app_Existing_result.Improvement.Trim() : app_Existing_result.Improvement;
            application_Financial.App_ExistingOwnerships.ErfSize = app_Existing_result.ErfSize;
            application_Financial.App_ExistingOwnerships.LeaseAggreement = !string.IsNullOrEmpty(app_Existing_result.LeaseAggreement) ? app_Existing_result.LeaseAggreement.Trim() : app_Existing_result.LeaseAggreement;
            application_Financial.App_ExistingOwnerships.WhoIsOccupyingTheErf = !string.IsNullOrEmpty(app_Existing_result.WhoIsOccupyingTheErf) ? app_Existing_result.WhoIsOccupyingTheErf.Trim() : app_Existing_result.WhoIsOccupyingTheErf;
            application_Financial.App_ExistingOwnerships.WhoIsResponsibleForPayment = !string.IsNullOrEmpty(app_Existing_result.WhoIsResponsibleForPayment) ? app_Existing_result.WhoIsResponsibleForPayment.Trim() : app_Existing_result.WhoIsResponsibleForPayment;
            application_Financial.App_ExistingOwnerships.OutstandingAmount = app_Existing_result.OutstandingAmount;
            application_Financial.App_ExistingOwnerships.ReferenceNumber = !string.IsNullOrEmpty(app_Existing_result.ReferenceNumber) ? app_Existing_result.ReferenceNumber.Trim() : app_Existing_result.ReferenceNumber;

            application_Financial.App_AttachmentDocuments = new App_AttachmentDocument();
            if (app_attachment_doc_result != null)
            {
                application_Financial.App_AttachmentDocuments.ApplicantID = app_Applicant.ApplicantID;
                application_Financial.App_AttachmentDocuments.AttachmentDocument = app_attachment_doc_result.AttachmentDocument;
                application_Financial.App_AttachmentDocuments.AttachmentDocumentFileName = app_attachment_doc_result.AttachmentDocumentFileName;
                application_Financial.App_AttachmentDocuments.AttachmentDocumentID = app_attachment_doc_result.AttachmentDocumentID;
                application_Financial.App_AttachmentDocuments.AttachmentDocumentTypeID = app_attachment_doc_result.AttachmentDocumentTypeID;
            }




            return View(application_Financial);
        }

        // POST:  Edit
        [HttpPost]
        public async Task<ActionResult> Update(App_Applicant App_Applicant, App_ExistingOwnership App_ExistingOwnership,
                            App_Financial App_Financials, App_AttachmentDocument app_AttachmentDocuments,
                             ApplicantFilesViewModel[] Id_Doc_Files, ApplicantFilesViewModel[] marriage_certificate_Files,
                              ApplicantFilesViewModel[] id_doc_spouse_Files, ApplicantFilesViewModel[] comp_reg_certificate_Files,
                               ApplicantFilesViewModel[] business_plan_Files, ApplicantFilesViewModel[] proof_of_funding_Files)
        {
            if (string.IsNullOrEmpty(App_Applicant.Firstname) || string.IsNullOrEmpty(App_Applicant.Surname))
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }

            string updatedBy = User.Identity.Name ?? "admin";
            DateTime updatedDate = DateTime.UtcNow;

            if (App_Applicant.FaxNumber == null)
            {
                App_Applicant.FaxNumber = "-";
            }




            try
            {
                var getLatestapplicant = db.App_Applicant.FirstOrDefault(x => x.ApplicantID == App_Applicant.ApplicantID);
                //getLatestapplicant.ApplicantID = App_Applicant.ApplicantID;
                getLatestapplicant.ApplicationDate = updatedDate; //App_Applicant.ApplicationDate;
                getLatestapplicant.App_ApplicantStatus = App_Applicant.App_ApplicantStatus;
                getLatestapplicant.UpdatedBy = updatedBy;
                getLatestapplicant.UpdatedDate = updatedDate;
                getLatestapplicant.EmailAddress = !string.IsNullOrEmpty(App_Applicant.EmailAddress) ? App_Applicant.EmailAddress.Trim() : App_Applicant.EmailAddress;
                getLatestapplicant.ErfSizeID = App_Applicant.ErfSizeID;
                getLatestapplicant.FaxNumber = !string.IsNullOrEmpty(App_Applicant.FaxNumber) ? App_Applicant.FaxNumber.Trim() : App_Applicant.FaxNumber;
                getLatestapplicant.Firstname = !string.IsNullOrEmpty(App_Applicant.Firstname) ? App_Applicant.Firstname.Trim() : App_Applicant.Firstname;
                getLatestapplicant.IDNumber = !string.IsNullOrEmpty(App_Applicant.IDNumber) ? App_Applicant.IDNumber.Trim() : App_Applicant.IDNumber;
                getLatestapplicant.IncomePerMonth = App_Applicant.IncomePerMonth;
                getLatestapplicant.NameOfEmployer = !string.IsNullOrEmpty(App_Applicant.NameOfEmployer) ? App_Applicant.NameOfEmployer.Trim() : App_Applicant.NameOfEmployer;
                getLatestapplicant.Occupation = !string.IsNullOrEmpty(App_Applicant.Occupation) ? App_Applicant.Occupation.Trim() : App_Applicant.Occupation;
                getLatestapplicant.PHOTO = App_Applicant.PHOTO;
                getLatestapplicant.PlotTypeID = App_Applicant.PlotTypeID == 0 ? 9 : App_Applicant.PlotTypeID;
                getLatestapplicant.PostalAddress = !string.IsNullOrEmpty(App_Applicant.PostalAddress) ? App_Applicant.PostalAddress.Trim() : App_Applicant.PostalAddress;
                getLatestapplicant.ResidentialAddress = !string.IsNullOrEmpty(App_Applicant.ResidentialAddress) ? App_Applicant.ResidentialAddress.Trim() : App_Applicant.ResidentialAddress;
                getLatestapplicant.SignedByApplicant = App_Applicant.SignedByApplicant;
                getLatestapplicant.SignedBySpouse = App_Applicant.SignedBySpouse;
                getLatestapplicant.SpouseEmailAddress = !string.IsNullOrEmpty(App_Applicant.SpouseEmailAddress) ? App_Applicant.SpouseEmailAddress.Trim() : App_Applicant.SpouseEmailAddress;
                getLatestapplicant.SpouseFaxNumber = !string.IsNullOrEmpty(App_Applicant.SpouseFaxNumber) ? App_Applicant.SpouseFaxNumber.Trim() : App_Applicant.SpouseFaxNumber;
                getLatestapplicant.SpouseFirstname = !string.IsNullOrEmpty(App_Applicant.SpouseEmailAddress) ? App_Applicant.SpouseFirstname.Trim() : App_Applicant.SpouseFirstname;
                getLatestapplicant.SpouseIDNumber = !string.IsNullOrEmpty(App_Applicant.SpouseIDNumber) ? App_Applicant.SpouseIDNumber.Trim() : App_Applicant.SpouseIDNumber;
                getLatestapplicant.SpouseIncomePerMonth = App_Applicant.SpouseIncomePerMonth;
                getLatestapplicant.SpouseNameOfEmployer = !string.IsNullOrEmpty(App_Applicant.SpouseNameOfEmployer) ? App_Applicant.SpouseNameOfEmployer.Trim() : App_Applicant.SpouseNameOfEmployer;
                getLatestapplicant.SpouseOccupation = !string.IsNullOrEmpty(App_Applicant.SpouseOccupation) ? App_Applicant.SpouseOccupation.Trim() : App_Applicant.SpouseOccupation;
                getLatestapplicant.SpousePostalAddress = !string.IsNullOrEmpty(App_Applicant.SpousePostalAddress) ? App_Applicant.SpousePostalAddress.Trim() : App_Applicant.SpousePostalAddress;
                getLatestapplicant.SpouseResidentialAddress = !string.IsNullOrEmpty(App_Applicant.SpouseResidentialAddress) ? App_Applicant.SpouseResidentialAddress.Trim() : App_Applicant.SpouseResidentialAddress;
                getLatestapplicant.SpouseSurname = !string.IsNullOrEmpty(App_Applicant.SpouseSurname) ? App_Applicant.SpouseSurname.Trim() : App_Applicant.SpouseSurname;
                getLatestapplicant.SpouseTelephoneNumber = !string.IsNullOrEmpty(App_Applicant.SpouseTelephoneNumber) ? App_Applicant.SpouseTelephoneNumber.Trim() : App_Applicant.SpouseTelephoneNumber;
                getLatestapplicant.StageAllocation = App_Applicant.StageAllocation;
                getLatestapplicant.StageApplication = App_Applicant.StageApplication;
                getLatestapplicant.StageRegistration = App_Applicant.StageRegistration;
                getLatestapplicant.StageSale = App_Applicant.StageSale;
                getLatestapplicant.Surname = !string.IsNullOrEmpty(App_Applicant.Surname) ? App_Applicant.Surname.Trim() : App_Applicant.Surname;
                getLatestapplicant.StageValuation = App_Applicant.StageValuation;
                getLatestapplicant.TelephoneNumber = !string.IsNullOrEmpty(App_Applicant.TelephoneNumber) ? App_Applicant.TelephoneNumber.Trim() : App_Applicant.TelephoneNumber;

                db.Entry(getLatestapplicant).State = EntityState.Modified;
                await db.SaveChangesAsync();

                var app_existing = db.App_ExistingOwnership.FirstOrDefault(x => x.ApplicantID == App_Applicant.ApplicantID);

                app_existing.ApplicantID = getLatestapplicant.ApplicantID;
                app_existing.DateOfFirstOccupation = App_ExistingOwnership.DateOfFirstOccupation;
                app_existing.ErfNumberID = App_ExistingOwnership.ErfNumberID;
                //app_existing.ExistingOwnershipID = app_ExistingOwnership.ExistingOwnershipID;
                app_existing.Improvement = !string.IsNullOrEmpty(App_ExistingOwnership.Improvement) ? App_ExistingOwnership.Improvement.Trim() : App_ExistingOwnership.Improvement;
                app_existing.ErfSize = !string.IsNullOrEmpty(App_ExistingOwnership.ErfSize) ? App_ExistingOwnership.ErfSize.Trim() : App_ExistingOwnership.ErfSize;
                app_existing.LeaseAggreement = !string.IsNullOrEmpty(App_ExistingOwnership.LeaseAggreement) ? App_ExistingOwnership.LeaseAggreement.Trim() : App_ExistingOwnership.LeaseAggreement;
                app_existing.WhoIsOccupyingTheErf = !string.IsNullOrEmpty(App_ExistingOwnership.WhoIsOccupyingTheErf) ? App_ExistingOwnership.WhoIsOccupyingTheErf.Trim() : App_ExistingOwnership.WhoIsOccupyingTheErf;
                app_existing.WhoIsResponsibleForPayment = !string.IsNullOrEmpty(App_ExistingOwnership.WhoIsResponsibleForPayment) ? App_ExistingOwnership.WhoIsResponsibleForPayment.Trim() : App_ExistingOwnership.WhoIsResponsibleForPayment;
                app_existing.OutstandingAmount = App_ExistingOwnership.OutstandingAmount;
                app_existing.ReferenceNumber = !string.IsNullOrEmpty(App_ExistingOwnership.ReferenceNumber) ? App_ExistingOwnership.ReferenceNumber.Trim() : App_ExistingOwnership.ReferenceNumber;
                app_existing.UpdatedBy = updatedBy;
                app_existing.UpdatedDate = updatedDate;

                db.Entry(app_existing).State = EntityState.Modified;
                await db.SaveChangesAsync();

                //App financial
                var get_latestApp_financial = db.App_Financial.FirstOrDefault(x => x.ApplicantID == App_Applicant.ApplicantID);

                get_latestApp_financial.UpdatedBy = updatedBy;
                get_latestApp_financial.UpdatedDate = updatedDate;
                get_latestApp_financial.ApplicantID = getLatestapplicant.ApplicantID;
                get_latestApp_financial.EstimatedCostOfConstruction = App_Financials.EstimatedCostOfConstruction;
                //get_latestApp_financial.FinancialID = app_financial.FinancialID;
                get_latestApp_financial.IntendedCommencementDate = App_Financials.IntendedCommencementDate;
                get_latestApp_financial.PaymentMethodID = App_Financials.PaymentMethodID;
                get_latestApp_financial.QualifiedLoanAmount = App_Financials.QualifiedLoanAmount;

                db.Entry(get_latestApp_financial).State = EntityState.Modified;
                await db.SaveChangesAsync();


                //Save Id Docs into Database

                ICollection<App_AttachmentDocument> attachments = new List<App_AttachmentDocument>();

                // Id Documents
                if (Id_Doc_Files != null && Id_Doc_Files.Count() > 0)
                {
                    foreach (var docFile in Id_Doc_Files)
                    {
                        attachments.Add(new App_AttachmentDocument()
                        {
                            ApplicantID = getLatestapplicant.ApplicantID,
                            AttachmentDocumentFileName = docFile.fileName.Trim(), //path,
                            AttachmentDocumentTypeID = 1, //app_AttachmentDocuments.AttachmentDocumentTypeID,
                            UpdatedBy = User.Identity.Name ?? "admin",
                            AttachmentDocument = Convert.FromBase64String(docFile.fileBase.Split(',')[1].Trim()),  //System.IO.File.ReadAllBytes(path)
                            UpdatedDate = DateTime.UtcNow
                        });
                    }
                }

                //Marriage Certificates
                if (marriage_certificate_Files != null && marriage_certificate_Files.Count() > 0)
                {
                    foreach (var docFile in marriage_certificate_Files)
                    {
                        attachments.Add(new App_AttachmentDocument()
                        {
                            ApplicantID = getLatestapplicant.ApplicantID,
                            AttachmentDocumentFileName = docFile.fileName.Trim(),
                            AttachmentDocumentTypeID = 2,
                            UpdatedBy = User.Identity.Name ?? "admin",
                            AttachmentDocument = Convert.FromBase64String(docFile.fileBase.Split(',')[1].Trim()),
                            UpdatedDate = DateTime.UtcNow
                        });
                    }
                }
                // Spuose documents
                if (id_doc_spouse_Files != null && id_doc_spouse_Files.Count() > 0)
                {
                    foreach (var docFile in id_doc_spouse_Files)
                    {
                        attachments.Add(new App_AttachmentDocument()
                        {
                            ApplicantID = getLatestapplicant.ApplicantID,
                            AttachmentDocumentFileName = docFile.fileName.Trim(),
                            AttachmentDocumentTypeID = 3,
                            UpdatedBy = User.Identity.Name ?? "admin",
                            AttachmentDocument = Convert.FromBase64String(docFile.fileBase.Split(',')[1].Trim()),
                            UpdatedDate = DateTime.UtcNow
                        });
                    }
                }

                // Company Reg. Files
                if (comp_reg_certificate_Files != null && comp_reg_certificate_Files.Count() > 0)
                {
                    foreach (var docFile in comp_reg_certificate_Files)
                    {
                        attachments.Add(new App_AttachmentDocument()
                        {
                            ApplicantID = getLatestapplicant.ApplicantID,
                            AttachmentDocumentFileName = docFile.fileName.Trim(),
                            AttachmentDocumentTypeID = 6,
                            UpdatedBy = User.Identity.Name ?? "admin",
                            AttachmentDocument = Convert.FromBase64String(docFile.fileBase.Split(',')[1].Trim()),
                            UpdatedDate = DateTime.UtcNow
                        });
                    }
                }
                // Business Plan Files
                if (business_plan_Files != null && business_plan_Files.Count() > 0)
                {
                    foreach (var docFile in business_plan_Files)
                    {
                        attachments.Add(new App_AttachmentDocument()
                        {
                            ApplicantID = getLatestapplicant.ApplicantID,
                            AttachmentDocumentFileName = docFile.fileName.Trim(),
                            AttachmentDocumentTypeID = 7,
                            UpdatedBy = User.Identity.Name ?? "admin",
                            AttachmentDocument = Convert.FromBase64String(docFile.fileBase.Split(',')[1].Trim()),
                            UpdatedDate = DateTime.UtcNow
                        });
                    }
                }


                // Proof of funding files
                if (proof_of_funding_Files != null && proof_of_funding_Files.Count() > 0)
                {
                    foreach (var docFile in proof_of_funding_Files)
                    {
                        attachments.Add(new App_AttachmentDocument()
                        {
                            ApplicantID = getLatestapplicant.ApplicantID,
                            AttachmentDocumentFileName = docFile.fileName.Trim(),
                            AttachmentDocumentTypeID = 8,
                            UpdatedBy = User.Identity.Name ?? "admin",
                            AttachmentDocument = Convert.FromBase64String(docFile.fileBase.Split(',')[1].Trim()),
                            UpdatedDate = DateTime.UtcNow
                        });
                    }
                }

                //Save/Update Attachment docs
                //foreach (var attc in attachments)
                // {
                if (attachments.Any())
                {
                    var getDocs = db.App_AttachmentDocument.Where(x => x.ApplicantID == App_Applicant.ApplicantID).ToList();
                    db.App_AttachmentDocument.RemoveRange(getDocs);
                    await db.SaveChangesAsync();

                    db.App_AttachmentDocument.AddRange(attachments);
                    await db.SaveChangesAsync();
                }

                //var getDocs = db.App_AttachmentDocument.FirstOrDefault(x => x.ApplicantID == attc.ApplicantID);
                //if (getDocs != null)
                //{
                //    //Update Existing
                //    getDocs.AttachmentDocumentFileName = attc.AttachmentDocumentFileName;
                //    getDocs.AttachmentDocument = attc.AttachmentDocument;

                //    getDocs.UpdatedBy = attc.UpdatedBy;
                //    getDocs.UpdatedDate = attc.UpdatedDate;

                //    db.Entry(getDocs).State = EntityState.Modified;
                //    await db.SaveChangesAsync();
                //}
                //else
                //{
                //    //Add New
                //    attc.CreatedBy = updatedBy;
                //    attc.CreatedDate = DateTime.UtcNow;
                //    db.App_AttachmentDocument.Add(attc);
                //    await db.SaveChangesAsync();
                //}
                //}
            }
            catch (Exception exx)
            {
                exx.GetBaseException();
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);


            //if (ModelState.IsValid)
            //{
            //    db.Entry(app_Applicant).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.ErfSizeID = new SelectList(db.Sys_ErfSize, "ErfSizeID", "ErfSize", app_Applicant.ErfSizeID);
            //ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType", app_Applicant.PlotTypeID);
            //return View(app_Applicant);
        }


        //Delete: POST
        [HttpPost]
        public JsonResult Delete(int id)
        {
            App_Applicant app_Applicant = db.App_Applicant.Find(id);
            if (app_Applicant != null)
            {
                var app_acknowledgement = db.App_AcknowledgementData.FirstOrDefault(x => x.ApplicantID == id);
                if (app_acknowledgement != null)
                    db.App_AcknowledgementData.Remove(app_acknowledgement);

                var app_waitinglist = db.App_WaitingList.FirstOrDefault(x => x.ApplicantID == id);
                if (app_waitinglist != null)
                    db.App_WaitingList.Remove(app_waitinglist);

                var app_ext = db.App_ExistingOwnership.FirstOrDefault(x => x.ApplicantID == id);
                if (app_ext != null)
                    db.App_ExistingOwnership.Remove(app_ext);

                var app_fin = db.App_Financial.FirstOrDefault(x => x.ApplicantID == id);
                if (app_fin != null)
                    db.App_Financial.Remove(app_fin);

                #region soft delete //
                var app_atachments = db.App_AttachmentDocument.Where(x => x.ApplicantID == id).ToList();

                //The below code performs the same task too 
                //var app_atachments = (from r in db.App_AttachmentDocument
                //                      where r.ApplicantID == id
                //                      select r).ToList();.
                #endregion

                if (app_atachments.Count() > 0)
                {
                    db.App_AttachmentDocument.RemoveRange(app_atachments);

                    //foreach (var item in app_atachments)
                    //{
                    //    db.App_AttachmentDocument.Remove(item);
                    //}

                }





                if (app_Applicant != null)
                    db.App_Applicant.Remove(app_Applicant);
                db.SaveChanges();
                return Json(new { status = true, message = "All linked records are also deleted." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message = "Current record does not exist in the database anymore" }, JsonRequestBehavior.AllowGet);
        }
        //GET
        [HttpGet]
        public JsonResult GetDocumentsByApplicantId(int app_id)//,int attachmentDocumentTypeId
        {
            var applicant_docs = db.App_AttachmentDocument.Where(x => x.ApplicantID == app_id && !string.IsNullOrEmpty(x.AttachmentDocumentFileName)).Select(x => new { x.AttachmentDocumentFileName, x.AttachmentDocument, x.AttachmentDocumentTypeID }).ToList();
            if (applicant_docs != null)
            {
                var returnData = Json(new { status = true, docs = applicant_docs }, JsonRequestBehavior.AllowGet);
                returnData.MaxJsonLength = (int?)50000000;
                return returnData;
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Generate reports
        public ActionResult ApplicantList()
        {
            return View(db.App_Applicant.ToList());
        }


        public ActionResult Reports(string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/ApplicantReport.rdlc");




            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "ApplicantDataSet";
            reportDataSource.Value = db.App_Applicant.ToList();
            localreport.DataSources.Add(reportDataSource);

            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;

            if (reportType == "Excel")
            {
                fileNameExtension = "xls";
            }

            else if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }

            else if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }

            else
            {
                fileNameExtension = "jpg";
            }

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;

            renderedByte = localreport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension
            , out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment ; filename= Applicant_report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);
            return View();
        }

    }
}
