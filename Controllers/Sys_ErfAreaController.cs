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
    public class Sys_ErfAreaController : Controller
    {
        private PMISEntities db = new PMISEntities();

        // GET: Sys_ErfArea
        public async Task<ActionResult> Index()
        {
            return View(await db.Sys_ErfArea.ToListAsync());
        }

        // GET: Sys_ErfArea/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfArea sys_ErfArea = await db.Sys_ErfArea.FindAsync(id);
            if (sys_ErfArea == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfArea);
        }

        // GET: Sys_ErfArea/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sys_ErfArea/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create([Bind(Include = "ErfAreaID,ErfArea")] Sys_ErfArea sys_ErfArea)
        {
            if (ModelState.IsValid)
            { sys_ErfArea.ErfArea.Trim();
                db.Sys_ErfArea.Add(sys_ErfArea);
                db.SaveChanges();
                return Json(new { status = true, message = "All linked records are also deleted." }, JsonRequestBehavior.AllowGet);
            }
            
                return Json(new { status = false, message = "Record could not be inserted , Please verify your inputs!"}, JsonRequestBehavior.AllowGet);

        }

        // GET: Sys_ErfArea/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfArea sys_ErfArea = await db.Sys_ErfArea.FindAsync(id);
            if (sys_ErfArea == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfArea);
        }

        // POST: Sys_ErfArea/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<JsonResult> Edit([Bind(Include = "ErfAreaID,ErfArea")] Sys_ErfArea sys_ErfArea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sys_ErfArea).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Json(new { status = true, message = "All linked records are also deleted." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message = "Record could not be inserted , Please verify your inputs!" }, JsonRequestBehavior.AllowGet);
        }

        // GET: Sys_ErfArea/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_ErfArea sys_ErfArea = await db.Sys_ErfArea.FindAsync(id);
            if (sys_ErfArea == null)
            {
                return HttpNotFound();
            }
            return View(sys_ErfArea);
        }

        // POST: Sys_ErfArea/Delete/5
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Delete(long id)
        {
            Sys_ErfArea sys_ErfArea = db.Sys_ErfArea.Find(id);
            if (ModelState.IsValid)
            {
                db.Sys_ErfArea.Remove(sys_ErfArea);
                db.SaveChanges();

                return Json(new { status = true, message = "All linked records are also deleted." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);

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
