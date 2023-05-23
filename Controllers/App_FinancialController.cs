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
    public class App_FinancialController : Controller
    {
        private PMISEntities db = new PMISEntities();

        // GET: App_Financial
        public ActionResult Index()
        {
            var app_Financial = db.App_Financial.Include(a => a.App_Applicant).Include(a => a.Sys_PaymentMethod);
            return View(app_Financial.ToList());
        }

        // GET: App_Financial/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_Financial app_Financial = db.App_Financial.Find(id);
            if (app_Financial == null)
            {
                return HttpNotFound();
            }
            return View(app_Financial);
        }

        // GET: App_Financial/Create
        public ActionResult Create()
        {
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname");
            ViewBag.PaymentMethodID = new SelectList(db.Sys_PaymentMethod, "PaymentMethodID", "PaymentMethod");
            return View();
        }

        // POST: App_Financial/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FinancialID,ApplicantID,PaymentMethodID,EstimatedCostOfConstruction,QualifiedLoanAmount,IntendedCommencementDate,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] App_Financial app_Financial)
        {
            if (ModelState.IsValid)
            {
                db.App_Financial.Add(app_Financial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_Financial.ApplicantID);
            ViewBag.PaymentMethodID = new SelectList(db.Sys_PaymentMethod, "PaymentMethodID", "PaymentMethod", app_Financial.PaymentMethodID);
            return View(app_Financial);
        }

        // GET: App_Financial/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_Financial app_Financial = db.App_Financial.Find(id);
            if (app_Financial == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_Financial.ApplicantID);
            ViewBag.PaymentMethodID = new SelectList(db.Sys_PaymentMethod, "PaymentMethodID", "PaymentMethod", app_Financial.PaymentMethodID);
            return View(app_Financial);
        }

        // POST: App_Financial/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FinancialID,ApplicantID,PaymentMethodID,EstimatedCostOfConstruction,QualifiedLoanAmount,IntendedCommencementDate,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] App_Financial app_Financial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(app_Financial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_Financial.ApplicantID);
            ViewBag.PaymentMethodID = new SelectList(db.Sys_PaymentMethod, "PaymentMethodID", "PaymentMethod", app_Financial.PaymentMethodID);
            return View(app_Financial);
        }

        // GET: App_Financial/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_Financial app_Financial = db.App_Financial.Find(id);
            if (app_Financial == null)
            {
                return HttpNotFound();
            }
            return View(app_Financial);
        }

        // POST: App_Financial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            App_Financial app_Financial = db.App_Financial.Find(id);
            db.App_Financial.Remove(app_Financial);
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
