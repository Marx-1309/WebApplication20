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
    public class Sys_ErfTownshipController : Controller
    {
        private PMISEntities db = new PMISEntities();

        // GET: Sys_ErfTownship
        public async Task<ActionResult> Index()
        {
            return View(await db.Sys_ErfTownship.ToListAsync());
        }

        // GET: Sys_ErfTownship/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfTownship sys_ErfTownship = await db.Sys_ErfTownship.FindAsync(id);
            if (sys_ErfTownship == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfTownship);
        }

        // GET: Sys_ErfTownship/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sys_ErfTownship/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<JsonResult> Create([Bind(Include = "ErfTownshipID,ErfTownship")] Sys_ErfTownship sys_ErfTownship)
        {
            if (ModelState.IsValid)
            {
                db.Sys_ErfTownship.Add(sys_ErfTownship);
                await db.SaveChangesAsync();
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Sys_ErfTownship/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfTownship sys_ErfTownship = await db.Sys_ErfTownship.FindAsync(id);
            sys_ErfTownship.ErfTownship.Trim();
            if (sys_ErfTownship == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfTownship);
        }

        // POST: Sys_ErfTownship/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<JsonResult> Edit([Bind(Include = "ErfTownshipID,ErfTownship")] Sys_ErfTownship sys_ErfTownship)
        {
            if (ModelState.IsValid)
            {
              
                db.Entry(sys_ErfTownship).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Json(new { status = true, message = "All linked records are also deleted." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message = "Record could not be inserted , Please verify your inputs!" }, JsonRequestBehavior.AllowGet);
        }
        // GET: Sys_ErfTownship/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfTownship sys_ErfTownship = await db.Sys_ErfTownship.FindAsync(id);
            if (sys_ErfTownship == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfTownship);
        }

        // POST: Sys_ErfTownship/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateInput(false)]
        public JsonResult Delete(long id)
        {
            Sys_ErfTownship sys_ErfTownship = db.Sys_ErfTownship.Find(id);
            db.Sys_ErfTownship.Remove(sys_ErfTownship);
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
