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
    public class App_WaitingListController : Controller
    {
        private PMISEntities db = new PMISEntities();

        // GET: App_WaitingList
        public ActionResult Index()
        {
            var app_WaitingList = db.App_WaitingList.Include(a => a.App_Applicant);
            return View(app_WaitingList.ToList());
        }

        // GET: App_WaitingList/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_WaitingList app_WaitingList = db.App_WaitingList.Find(id);
            if (app_WaitingList == null)
            {
                return HttpNotFound();
            }
            return View(app_WaitingList);
        }

        // GET: App_WaitingList/Create
        public ActionResult Create()
        {
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname");
            return View();
        }

        // POST: App_WaitingList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WaitingListID,WaitingListNumber,ApplicantID,UserDefined1,UserDefined2")] App_WaitingList app_WaitingList)
        {
            if (ModelState.IsValid)
            {
                db.App_WaitingList.Add(app_WaitingList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_WaitingList.ApplicantID);
            return View(app_WaitingList);
        }

        // GET: App_WaitingList/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_WaitingList app_WaitingList = db.App_WaitingList.Find(id);
            if (app_WaitingList == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_WaitingList.ApplicantID);
            return View(app_WaitingList);
        }

        // POST: App_WaitingList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WaitingListID,WaitingListNumber,ApplicantID,UserDefined1,UserDefined2")] App_WaitingList app_WaitingList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(app_WaitingList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicantID = new SelectList(db.App_Applicant, "ApplicantID", "Surname", app_WaitingList.ApplicantID);
            return View(app_WaitingList);
        }

        // GET: App_WaitingList/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_WaitingList app_WaitingList = db.App_WaitingList.Find(id);
            if (app_WaitingList == null)
            {
                return HttpNotFound();
            }
            return View(app_WaitingList);
        }

        // POST: App_WaitingList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            App_WaitingList app_WaitingList = db.App_WaitingList.Find(id);
            db.App_WaitingList.Remove(app_WaitingList);
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
