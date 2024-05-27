using Microsoft.Ajax.Utilities;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using WebApplication20.Models;
using WebApplication20.VeiwModel;
using static System.Data.Entity.Validation.DbEntityValidationException;

namespace WebApplication20.Controllers
{
    
    public class App_ErfController : Controller
    {
        private PMISEntities db = new PMISEntities();

        #region savingDbToVm
        public ActionResult AppErfVmDb()
        {
    
            AppErfVM appErfVM = new AppErfVM();
            App_Erf app_Erf = new App_Erf();
            appErfVM.ErfNumberID = app_Erf.ErfNumberID;
            appErfVM.ErfNumber = app_Erf.ErfNumber;
            appErfVM.ErfSize = app_Erf.ErfSize;
            appErfVM.ErfAreaID = app_Erf.ErfAreaID;
            appErfVM.PlotTypeID = app_Erf.PlotTypeID;
            appErfVM.ErfTownshipID = app_Erf.ErfTownshipID;
            appErfVM.ErfComment = app_Erf.ErfComment;
            appErfVM.CreatedBy = app_Erf.CreatedBy;
            appErfVM.CreatedDate = app_Erf.CreatedDate;
            appErfVM.UpdatedBy = app_Erf.UpdatedBy;
            appErfVM.UpdatedDate = app_Erf.UpdatedDate;
            appErfVM.ErfPurchasePrice = app_Erf.ErfPurchasePrice;
            appErfVM.ErfAdminFees = app_Erf.ErfAdminFees;
            appErfVM.ErfVat = app_Erf.ErfVat;
            appErfVM.StageAllocation = app_Erf.StageAllocation;
            appErfVM.StageSale = app_Erf.StageSale;
            appErfVM.StageRegistration = app_Erf.StageRegistration;
            appErfVM.StageValuation = app_Erf.StageValuation;
            appErfVM.ErfTotalPurchasePrice = app_Erf.ErfPurchasePrice;

            return null;
        }
        #endregion


        // GET: App_Erf
        public ActionResult Index()
        {
            //Sys_ErfTownship _ErfTownship = new Sys_ErfTownship();
            //App_Erf _app_Erf = new App_Erf();
            //_app_Erf.ErfTownshipID = _ErfTownship.ErfTownshipID;
            //System.Threading.Thread.Sleep(5000);
            var app_Erf = db.App_Erf.Include(a => a.Sys_ErfArea).Include(a => a.Sys_ErfTownship).Include(a => a.Sys_PlotType);
            ViewBag.mssg = TempData["mssg"] as string;
            return View(app_Erf.ToList());
            
        }

        // GET: App_Erf/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_Erf app_Erf = db.App_Erf.Find(id);
            if (app_Erf == null)
            {
                return HttpNotFound();
            }
            return View(app_Erf);
        }

        // GET: App_Erf/Create
        public ActionResult Create()
        {
            ViewBag.ErfAreaID = new SelectList(db.Sys_ErfArea, "ErfAreaID", "ErfArea");
            ViewBag.ErfTownshipID = new SelectList(db.Sys_ErfTownship, "ErfTownshipID", "ErfTownship");
            ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType");
            return View();
        }

        // POST: App_Erf/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create([Bind(Include = "ErfNumberID,ErfNumber,ErfSize,ErfAreaID,PlotTypeID,ErfTownshipID,ErfComment,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,ErfPurchasePrice,ErfAdminFees,ErfVat,StageAllocation,StageSale,StageRegistration,StageValuation,ErfTotalPurchasePrice")] App_Erf app_Erf)
        {
            App_Erf app_ = new App_Erf();
            AppErfVM appErfVM = new AppErfVM();
            string createdBy = User.Identity.Name ?? "admin";
            DateTime? createdDate = DateTime.Now;

            if (db.App_Erf.Any(a => a.ErfNumber.Equals(app_Erf.ErfNumber) &&
            a.ErfAreaID.Equals(app_Erf.ErfAreaID) && a.ErfTownshipID.Equals(app_Erf.ErfTownshipID) && a.ErfNumberID != app_Erf.ErfNumberID))
            {
                ModelState.AddModelError("ErfNumber", "ErfNumber already exist in this extension! :" + app_Erf.ErfNumber);
            }

            app_Erf.CreatedBy = createdBy;
            app_Erf.CreatedDate = createdDate;

            if (app_Erf.ErfComment == null)
            {
                app_Erf.ErfComment = "(No comments)";
            }

            if (app_Erf.StageValuation == null)
            { app_Erf.StageValuation = false; }
            if (app_Erf.StageAllocation == null)
            { app_Erf.StageAllocation = false; }
            if (app_Erf.StageRegistration == null)
            { app_Erf.StageRegistration = false; }
            if (app_Erf.StageSale == null)
            { app_Erf.StageSale = false; }
           


            if (ModelState.IsValid)
            {
                appErfVM.ErfNumberID = app_Erf.ErfNumberID;
                appErfVM.ErfNumber = app_Erf.ErfNumber.Trim();
                appErfVM.ErfSize = app_Erf.ErfSize;
                appErfVM.ErfAreaID = app_Erf.ErfAreaID;
                appErfVM.PlotTypeID = app_Erf.PlotTypeID;
                appErfVM.ErfTownshipID = app_Erf.ErfTownshipID;
                appErfVM.ErfComment = app_Erf.ErfComment.Trim();
                appErfVM.CreatedBy = app_Erf.CreatedBy = createdBy.Trim();
                appErfVM.CreatedDate = app_Erf.CreatedDate = createdDate;
                appErfVM.ErfPurchasePrice = app_Erf.ErfPurchasePrice;
                appErfVM.ErfAdminFees = app_Erf.ErfAdminFees;
                appErfVM.ErfVat = app_Erf.ErfVat;
                appErfVM.StageAllocation = app_Erf.StageAllocation;
                appErfVM.StageSale = app_Erf.StageSale;
                appErfVM.StageRegistration = app_Erf.StageRegistration;
                appErfVM.StageValuation = app_Erf.StageValuation;
                appErfVM.ErfTotalPurchasePrice = app_Erf.ErfPurchasePrice;
                db.App_Erf.Add(app_Erf);
                db.SaveChanges();

                TempData["mssg"] = "Thank you.";

                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ErfAreaID = new SelectList(db.Sys_ErfArea, "ErfAreaID", "ErfArea", app_Erf.ErfAreaID);
            ViewBag.ErfTownshipID = new SelectList(db.Sys_ErfTownship, "ErfTownshipID", "ErfTownship", app_Erf.ErfTownshipID);
            ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType", app_Erf.PlotTypeID);
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        //Remote validation
        public JsonResult IsErfNumberAvailable(String ErfComment)
        {
            return Json(!db.App_Erf.Any(x => x.ErfComment == ErfComment),
                                                 JsonRequestBehavior.AllowGet);
        }

       


        #region 
        // GET: App_Erf/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_Erf app_Erf = db.App_Erf.Find(id);
            if (app_Erf == null)
            {
                return HttpNotFound();
            }
            ViewBag.ErfAreaID = new SelectList(db.Sys_ErfArea, "ErfAreaID", "ErfArea", app_Erf.ErfAreaID);
            ViewBag.ErfTownshipID = new SelectList(db.Sys_ErfTownship, "ErfTownshipID", "ErfTownship", app_Erf.ErfTownshipID);
            ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType", app_Erf.PlotTypeID);
            return View(app_Erf);
        }
        #endregion
        // POST: App_Erf/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(App_Erf app_Erf)
        {
            App_Erf app_ = new App_Erf();
            AppErfVM appErfVM = new AppErfVM();
            string updatedBy = User.Identity.Name ?? "admin";
            DateTime updatedDate = DateTime.UtcNow;

            if (db.App_Erf.Any(a => a.ErfNumber.Equals(app_Erf.ErfNumber) && a.ErfAreaID.Equals(app_Erf.ErfAreaID) && a.ErfTownshipID.Equals(app_Erf.ErfTownshipID) && a.ErfNumberID != app_Erf.ErfNumberID))
            {
                ModelState.AddModelError("ErfNumber", "ErfNumber already exist in this extension! :" + app_Erf.ErfNumber);
            }


            //if (db.students.Any(a => a.Name.Equals(student.Name) && a.Id != student.Id))
            //{
            //    // ...
            //}


            if (ModelState.IsValid)
            {
               
                try { 
                App_Erf Erven = db.App_Erf.SingleOrDefault(x => x.ErfNumberID == app_Erf.ErfNumberID);
                    
                    if (app_Erf.StageValuation == null)
                    { app_Erf.StageValuation = false; }
                    if (app_Erf.StageAllocation == null)
                    { app_Erf.StageAllocation = false; }
                    if (app_Erf.StageRegistration == null)
                    { app_Erf.StageRegistration = false; }
                    if (app_Erf.StageSale == null)
                    { app_Erf.StageSale = false; }
                    //New 
                    Erven.ErfNumberID = appErfVM.ErfNumberID =app_Erf.ErfNumberID ;
                Erven.ErfNumber = appErfVM.ErfNumber = app_Erf.ErfNumber;
                Erven.ErfSize = appErfVM.ErfSize = app_Erf.ErfSize;
                Erven.ErfAreaID = appErfVM.ErfAreaID = app_Erf.ErfAreaID;
                Erven.PlotTypeID = appErfVM.PlotTypeID = app_Erf.PlotTypeID;
                Erven.Sys_PlotType = app_Erf.Sys_PlotType;
                Erven.ErfTownshipID = appErfVM.ErfTownshipID = app_Erf.ErfTownshipID ;
                Erven.ErfComment = appErfVM.ErfComment = app_Erf.ErfComment;
                Erven.CreatedBy = appErfVM.CreatedBy = app_Erf.CreatedBy;
                Erven.CreatedDate = appErfVM.CreatedDate = app_Erf.CreatedDate;
                Erven.UpdatedBy = appErfVM.UpdatedBy = app_Erf.UpdatedBy="Admin";
                Erven.UpdatedDate = appErfVM.UpdatedDate = app_Erf.UpdatedDate=updatedDate;
                Erven.ErfPurchasePrice = appErfVM.ErfPurchasePrice = app_Erf.ErfPurchasePrice;
                Erven.ErfAdminFees = appErfVM.ErfAdminFees = app_Erf.ErfAdminFees;
                Erven.ErfVat = appErfVM.ErfVat = app_Erf.ErfVat;
                Erven.StageAllocation = appErfVM.StageAllocation = app_Erf.StageAllocation;
                Erven.StageSale = appErfVM.StageSale = app_Erf.StageSale;
                Erven.StageRegistration = appErfVM.StageRegistration = app_Erf.StageRegistration;
                Erven.StageValuation= appErfVM.StageValuation = app_Erf.StageValuation;
                Erven.ErfTotalPurchasePrice = appErfVM.ErfTotalPurchasePrice = app_Erf.ErfPurchasePrice;

                db.SaveChanges();
                }
                catch (Exception exx)
                {
                    exx.GetBaseException();
                    return Json(new { status = false }, JsonRequestBehavior.AllowGet);

                }
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);

                //return RedirectToAction("Index");
            }
            ViewBag.ErfAreaID = new SelectList(db.Sys_ErfArea, "ErfAreaID", "ErfArea", app_Erf.ErfAreaID);
            ViewBag.ErfTownshipID = new SelectList(db.Sys_ErfTownship, "ErfTownshipID", "ErfTownship", app_Erf.ErfTownshipID);
            ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType", app_Erf.PlotTypeID);
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: App_Erf/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_Erf app_Erf = db.App_Erf.Find(id);
            if (app_Erf == null)
            {
                return HttpNotFound();
            }
            return View(app_Erf);
        }

        // POST: App_Erf/Delete/5
        [HttpPost/*, ActionName("Delete")*/]
        [ValidateInput(false)]


        public JsonResult Delete(long id)
        {
            App_Erf app_Erf = db.App_Erf.Find(id);
            db.App_Erf.Remove(app_Erf);
            db.SaveChanges();
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);

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
        public ActionResult ErfList()
        {
            return View(db.App_Erf.ToList());
        }


        public ActionResult Reports(string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/AppErfReport.rdlc");




            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "AppErf";
            reportDataSource.Value = db.App_Erf.ToList();
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

            Response.AddHeader("content-disposition", "attachment; filename= apperf_report." + fileNameExtension);

            
            return File(renderedByte, fileNameExtension);
            return View();
        }
    }
}
