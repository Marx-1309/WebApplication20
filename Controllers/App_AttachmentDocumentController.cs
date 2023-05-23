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
    public class App_AttachmentDocumentController : Controller
    {
        private PMISEntities db = new PMISEntities();

        // GET: App_AttachmentDocument
        public ActionResult Index()
        {
            var app_AttachmentDocument = db.App_AttachmentDocument.Include(a => a.App_Applicant).Include(a => a.Sys_AttachmentDocumentType);
            return View(app_AttachmentDocument.ToList());
        }

        // GET: App_AttachmentDocument/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_AttachmentDocument app_AttachmentDocument = db.App_AttachmentDocument.Find(id);
            if (app_AttachmentDocument == null)
            {
                return HttpNotFound();
            }
            return View(app_AttachmentDocument);
        }

        // GET: App_AttachmentDocument/Create
        public ActionResult Create()
        {
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname");
            ViewBag.AttachmentDocumentTypeID = new SelectList(db.Sys_AttachmentDocumentType, "AttachmentDocumentTypeID", "AttachmentDocumentType");
            return View();
        }

        // POST: App_AttachmentDocument/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttachmentDocumentID,ApplicantID,AttachmentDocumentTypeID,AttachmentDocument,AttachmentDocumentFileName,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] App_AttachmentDocument app_AttachmentDocument)
        {
            if (ModelState.IsValid)
            {
                db.App_AttachmentDocument.Add(app_AttachmentDocument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_AttachmentDocument.ApplicantID);
            ViewBag.AttachmentDocumentTypeID = new SelectList(db.Sys_AttachmentDocumentType, "AttachmentDocumentTypeID", "AttachmentDocumentType", app_AttachmentDocument.AttachmentDocumentTypeID);
            return View(app_AttachmentDocument);
        }

        // GET: App_AttachmentDocument/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_AttachmentDocument app_AttachmentDocument = db.App_AttachmentDocument.Find(id);
            if (app_AttachmentDocument == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_AttachmentDocument.ApplicantID);
            ViewBag.AttachmentDocumentTypeID = new SelectList(db.Sys_AttachmentDocumentType, "AttachmentDocumentTypeID", "AttachmentDocumentType", app_AttachmentDocument.AttachmentDocumentTypeID);
            return View(app_AttachmentDocument);
        }

        // POST: App_AttachmentDocument/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttachmentDocumentID,ApplicantID,AttachmentDocumentTypeID,AttachmentDocument,AttachmentDocumentFileName,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] App_AttachmentDocument app_AttachmentDocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(app_AttachmentDocument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_AttachmentDocument.ApplicantID);
            ViewBag.AttachmentDocumentTypeID = new SelectList(db.Sys_AttachmentDocumentType, "AttachmentDocumentTypeID", "AttachmentDocumentType", app_AttachmentDocument.AttachmentDocumentTypeID);
            return View(app_AttachmentDocument);
        }

        // GET: App_AttachmentDocument/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_AttachmentDocument app_AttachmentDocument = db.App_AttachmentDocument.Find(id);
            if (app_AttachmentDocument == null)
            {
                return HttpNotFound();
            }
            return View(app_AttachmentDocument);
        }

        // POST: App_AttachmentDocument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            App_AttachmentDocument app_AttachmentDocument = db.App_AttachmentDocument.Find(id);
            db.App_AttachmentDocument.Remove(app_AttachmentDocument);
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
