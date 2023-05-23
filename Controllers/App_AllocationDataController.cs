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
    public class App_AllocationDataController : Controller
    {
        private PMISEntities db = new PMISEntities();

        // GET: App_AllocationData
        public ActionResult Index()
        {
            var app_AllocationData = db.App_AllocationData.Include(a => a.App_Allocation).Include(a => a.App_Applicant).Include(a => a.App_Erf);
            return View(app_AllocationData.ToList());
        }

        // GET: App_AllocationData/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_AllocationData app_AllocationData = db.App_AllocationData.Find(id);
            if (app_AllocationData == null)
            {
                return HttpNotFound();
            }
            return View(app_AllocationData);
        }

        // GET: App_AllocationData/Create
        public ActionResult Create()
        {
            ViewBag.AllocationID = new SelectList(db.App_Allocation, "AllocationID", "AllocationLetterFileName");
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname");
            ViewBag.ErfNumberID = new SelectList(db.App_Erf, "ErfNumberID", "ErfNumber");
            return View();
        }

        // POST: App_AllocationData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AllocationDataID,AllocationID,ApplicantID,ErfNumberID,AllocationReason,AllocationLetter,AllocationLetterFileName,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] App_AllocationData app_AllocationData)
        {
            if (ModelState.IsValid)
            {
                db.App_AllocationData.Add(app_AllocationData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllocationID = new SelectList(db.App_Allocation, "AllocationID", "AllocationLetterFileName", app_AllocationData.AllocationID);
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_AllocationData.ApplicantID);
            ViewBag.ErfNumberID = new SelectList(db.App_Erf, "ErfNumberID", "ErfNumber", app_AllocationData.ErfNumberID);
            return View(app_AllocationData);
        }

        // GET: App_AllocationData/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_AllocationData app_AllocationData = db.App_AllocationData.Find(id);
            if (app_AllocationData == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllocationID = new SelectList(db.App_Allocation, "AllocationID", "AllocationLetterFileName", app_AllocationData.AllocationID);
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_AllocationData.ApplicantID);
            ViewBag.ErfNumberID = new SelectList(db.App_Erf, "ErfNumberID", "ErfNumber", app_AllocationData.ErfNumberID);
            return View(app_AllocationData);
        }

        // POST: App_AllocationData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AllocationDataID,AllocationID,ApplicantID,ErfNumberID,AllocationReason,AllocationLetter,AllocationLetterFileName,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] App_AllocationData app_AllocationData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(app_AllocationData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllocationID = new SelectList(db.App_Allocation, "AllocationID", "AllocationLetterFileName", app_AllocationData.AllocationID);
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_AllocationData.ApplicantID);
            ViewBag.ErfNumberID = new SelectList(db.App_Erf, "ErfNumberID", "ErfNumber", app_AllocationData.ErfNumberID);
            return View(app_AllocationData);
        }

        // GET: App_AllocationData/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_AllocationData app_AllocationData = db.App_AllocationData.Find(id);
            if (app_AllocationData == null)
            {
                return HttpNotFound();
            }
            return View(app_AllocationData);
        }

        // POST: App_AllocationData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            App_AllocationData app_AllocationData = db.App_AllocationData.Find(id);
            db.App_AllocationData.Remove(app_AllocationData);
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
