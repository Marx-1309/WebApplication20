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
    public class Sys_ErfZoningController : Controller
    {
        private PMISEntities db = new PMISEntities();

        // GET: Sys_ErfZoning
        public async Task<ActionResult> Index()
        {
            return View(await db.Sys_ErfZoning.ToListAsync());
        }

        // GET: Sys_ErfZoning/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfZoning sys_ErfZoning = await db.Sys_ErfZoning.FindAsync(id);
            if (sys_ErfZoning == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfZoning);
        }

        // GET: Sys_ErfZoning/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sys_ErfZoning/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Create([Bind(Include = "ErfZoningID,ErfZoning")] Sys_ErfZoning sys_ErfZoning)
        {
            if (ModelState.IsValid)
            {
                db.Sys_ErfZoning.Add(sys_ErfZoning);
                await db.SaveChangesAsync();
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        // GET: Sys_ErfZoning/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfZoning sys_ErfZoning = await db.Sys_ErfZoning.FindAsync(id);
            if (sys_ErfZoning == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfZoning);
        }

        // POST: Sys_ErfZoning/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<JsonResult> Edit([Bind(Include = "ErfZoningID,ErfZoning")] Sys_ErfZoning sys_ErfZoning)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sys_ErfZoning).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Json(new { status = true, message = "All linked records are also deleted." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message = "Record could not be inserted , Please verify your inputs!" }, JsonRequestBehavior.AllowGet);
        }

        // GET: Sys_ErfZoning/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfZoning sys_ErfZoning = await db.Sys_ErfZoning.FindAsync(id);
            if (sys_ErfZoning == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfZoning);
        }

        // POST: Sys_ErfZoning/Delete/5
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Delete(long id)
        {
            Sys_ErfZoning sys_ErfZoning = db.Sys_ErfZoning.Find(id);
            db.Sys_ErfZoning.Remove(sys_ErfZoning);
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
