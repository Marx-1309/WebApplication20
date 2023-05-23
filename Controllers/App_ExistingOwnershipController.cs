using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication20.Models;

namespace WebApplication20.Controllers
{
    public class App_ExistingOwnershipController : Controller
    {
        private PMISEntities db = new PMISEntities();

        // GET: App_ExistingOwnership
        public ActionResult Index()
        {
            var app_ExistingOwnership = db.App_ExistingOwnership.Include(a => a.App_Applicant).Include(a => a.App_Erf);
            return View(app_ExistingOwnership.ToList());
        }

        // GET: App_ExistingOwnership/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_ExistingOwnership app_ExistingOwnership = db.App_ExistingOwnership.Find(id);
            if (app_ExistingOwnership == null)
            {
                return HttpNotFound();
            }
            return View(app_ExistingOwnership);
        }

        // GET: App_ExistingOwnership/Create
        public ActionResult Create()

        {

            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname");
            ViewBag.ErfNumberID = new SelectList(db.App_Erf, "ErfNumberID", "ErfNumber");
            return View();
        }

        // POST: App_ExistingOwnership/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "ExistingOwnershipID,ErfNumberID,ApplicantID,ErfSize,ReferenceNumber,Improvement,LeaseAggreement,OutstandingAmount,DateOfFirstOccupation,WhoIsOccupyingTheErf,WhoIsResponsibleForPayment,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] App_ExistingOwnership app_ExistingOwnership)
        {
            if (ModelState.IsValid)
            {
                db.App_ExistingOwnership.Add(app_ExistingOwnership);
                db.SaveChanges();
                return Json(new { status = true, message = "All linked records are also deleted." }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_ExistingOwnership.ApplicantID);
            ViewBag.ErfNumberID = new SelectList(db.App_Erf, "ErfNumberID", "ErfNumber", app_ExistingOwnership.ErfNumberID);
            return Json(new { status = true, message = "All linked records are also deleted." }, JsonRequestBehavior.AllowGet);
        }

        // GET: App_ExistingOwnership/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_ExistingOwnership app_ExistingOwnership = db.App_ExistingOwnership.Find(id);
            if (app_ExistingOwnership == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_ExistingOwnership.ApplicantID);
            ViewBag.ErfNumberID = new SelectList(db.App_Erf, "ErfNumberID", "ErfNumber", app_ExistingOwnership.ErfNumberID);
            return View(app_ExistingOwnership);
        }

        // POST: App_ExistingOwnership/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExistingOwnershipID,ApplicantID,ErfNumberID,ErfSize,ReferenceNumber,Improvement,LeaseAggreement,OutstandingAmount,DateOfFirstOccupation,WhoIsOccupyingTheErf,WhoIsResponsibleForPayment,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] App_ExistingOwnership app_ExistingOwnership)
        {
            if (ModelState.IsValid)
            {
                db.Entry(app_ExistingOwnership).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_ExistingOwnership.ApplicantID);
            ViewBag.ErfNumberID = new SelectList(db.App_Erf, "ErfNumberID", "ErfNumber", app_ExistingOwnership.ErfNumberID);
            return View(app_ExistingOwnership);
        }

        // GET: App_ExistingOwnership/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_ExistingOwnership app_ExistingOwnership = db.App_ExistingOwnership.Find(id);
            if (app_ExistingOwnership == null)
            {
                return HttpNotFound();
            }
            return View(app_ExistingOwnership);
        }

        // POST: App_ExistingOwnership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            App_ExistingOwnership app_ExistingOwnership = db.App_ExistingOwnership.Find(id);
            db.App_ExistingOwnership.Remove(app_ExistingOwnership);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
