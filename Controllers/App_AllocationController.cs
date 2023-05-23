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
    public class App_AllocationController : Controller
    {
        private PMISEntities db = new PMISEntities();

        // GET: App_Allocation
        public ActionResult Index()
        {
            var app_Allocation = db.App_Allocation.Include(a => a.Sys_Month).Include(a => a.Sys_PlotType);
            return View(app_Allocation.ToList());
        }

        // GET: App_Allocation/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_Allocation app_Allocation = db.App_Allocation.Find(id);
            if (app_Allocation == null)
            {
                return HttpNotFound();
            }
            return View(app_Allocation);
        }

        // GET: App_Allocation/Create
        public ActionResult Create()
        {
            ViewBag.MonthID = new SelectList(db.Sys_Month, "MonthID", "MonthName");
            ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType");
            return View();
        }

        // POST: App_Allocation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AllocationID,PlotTypeID,MonthID,AllocationYear,AllocationDate,AllocationLetter,AllocationLetterFileName,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] App_Allocation app_Allocation)
        {
            if (ModelState.IsValid)
            {
                db.App_Allocation.Add(app_Allocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MonthID = new SelectList(db.Sys_Month, "MonthID", "MonthName", app_Allocation.MonthID);
            ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType", app_Allocation.PlotTypeID);
            return View(app_Allocation);
        }

        // GET: App_Allocation/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_Allocation app_Allocation = db.App_Allocation.Find(id);
            if (app_Allocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.MonthID = new SelectList(db.Sys_Month, "MonthID", "MonthName", app_Allocation.MonthID);
            ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType", app_Allocation.PlotTypeID);
            return View(app_Allocation);
        }

        // POST: App_Allocation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AllocationID,PlotTypeID,MonthID,AllocationYear,AllocationDate,AllocationLetter,AllocationLetterFileName,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] App_Allocation app_Allocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(app_Allocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MonthID = new SelectList(db.Sys_Month, "MonthID", "MonthName", app_Allocation.MonthID);
            ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType", app_Allocation.PlotTypeID);
            return View(app_Allocation);
        }

        // GET: App_Allocation/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App_Allocation app_Allocation = db.App_Allocation.Find(id);
            if (app_Allocation == null)
            {
                return HttpNotFound();
            }
            return View(app_Allocation);
        }

        // POST: App_Allocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            App_Allocation app_Allocation = db.App_Allocation.Find(id);
            db.App_Allocation.Remove(app_Allocation);
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
