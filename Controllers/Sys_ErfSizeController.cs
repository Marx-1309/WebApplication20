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
    public class Sys_ErfSizeController : Controller
    {
        private PMISEntities db = new PMISEntities();

        // GET: Sys_ErfSize
        public async Task<ActionResult> Index()
        {
            return View(await db.Sys_ErfSize.ToListAsync());
        }

        // GET: Sys_ErfSize/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfSize sys_ErfSize = await db.Sys_ErfSize.FindAsync(id);
            if (sys_ErfSize == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfSize);
        }

        // GET: Sys_ErfSize/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sys_ErfSize/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Create([Bind(Include = "ErfSizeID,ErfSize")] Sys_ErfSize sys_ErfSize)
        {
            if (ModelState.IsValid)
            {
                db.Sys_ErfSize.Add(sys_ErfSize);
                await db.SaveChangesAsync();
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Sys_ErfSize/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfSize sys_ErfSize = await db.Sys_ErfSize.FindAsync(id);
            if (sys_ErfSize == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfSize);
        }

        // POST: Sys_ErfSize/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<JsonResult> Edit([Bind(Include = "ErfSizeID,ErfSize")] Sys_ErfSize sys_ErfSize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sys_ErfSize).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Json(new { status = true}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message = "Record could not be inserted , Please verify your inputs!" }, JsonRequestBehavior.AllowGet);
        }

        // GET: Sys_ErfSize/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfSize sys_ErfSize = await db.Sys_ErfSize.FindAsync(id);
            if (sys_ErfSize == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfSize);
        }

        // POST: Sys_ErfSize/Delete/5
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Delete(long id)
        {
            Sys_ErfSize sys_ErfSize = db.Sys_ErfSize.Find(id);
            db.Sys_ErfSize.Remove(sys_ErfSize);
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
