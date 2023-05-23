using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication20.Models;

namespace WebApplication20.Controllers
{
    public class Sys_ErfCostController : Controller
    {
        private PMISEntities db = new PMISEntities();

        // GET: Sys_ErfCost
        public async Task<ActionResult> Index()
        {
            var sys_ErfCost = db.Sys_ErfCost.Include(s => s.Sys_PlotType);
            return View(await sys_ErfCost.ToListAsync());
        }

        // GET: Sys_ErfCost/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfCost sys_ErfCost = await db.Sys_ErfCost.FindAsync(id);
            if (sys_ErfCost == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfCost);
        }

        // GET: Sys_ErfCost/Create
        public ActionResult Create()
        {
            ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType");
            return View();
        }

        // POST: Sys_ErfCost/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Create([Bind(Include = "ErfCostID,PlotTypeID,CostPerSqm,AdminFees")] Sys_ErfCost sys_ErfCost)
        {
            if (ModelState.IsValid)
            {
                db.Sys_ErfCost.Add(sys_ErfCost);
                await db.SaveChangesAsync();
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType", sys_ErfCost.PlotTypeID);
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Sys_ErfCost/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfCost sys_ErfCost = await db.Sys_ErfCost.FindAsync(id);
            if (sys_ErfCost == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlotTypeID = new SelectList(db.Sys_PlotType, "PlotTypeID", "PlotType", sys_ErfCost.PlotTypeID);
            return View(sys_ErfCost);
        }

        // POST: Sys_ErfCost/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateInput(false)]
        public async Task<JsonResult> Edit([Bind(Include = "ErfCostID,PlotTypeID,CostPerSqm,AdminFees")] Sys_ErfCost sys_ErfCost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sys_ErfCost).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Json(new { status = true, message = "All linked records are also deleted." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message = "Record could not be inserted , Please verify your inputs!" }, JsonRequestBehavior.AllowGet);
        }

        // GET: Sys_ErfCost/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfCost sys_ErfCost = await db.Sys_ErfCost.FindAsync(id);
            if (sys_ErfCost == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfCost);
        }

        // POST: Sys_ErfCost/Delete/5
        [HttpPost/*, ActionName("Delete")*/]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult Delete(long id)
        {
            Sys_ErfCost sys_ErfCost = db.Sys_ErfCost.Find(id);
            db.Sys_ErfCost.Remove(sys_ErfCost);
            db.SaveChanges();
            return Json(new { status = true, message = "All linked records are also deleted." }, JsonRequestBehavior.AllowGet);
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
