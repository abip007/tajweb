using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class BusinessTypesController : Controller
    {
        private MMS db = new MMS();

        // GET: BusinessTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.BusinessType.ToListAsync());
        }

        // GET: BusinessTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessType businessType = await db.BusinessType.FindAsync(id);
            if (businessType == null)
            {
                return HttpNotFound();
            }
            return View(businessType);
        }

        // GET: BusinessTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "btypeid,btype")] BusinessType businessType)
        {
            if (ModelState.IsValid)
            {
                db.BusinessType.Add(businessType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(businessType);
        }

        // GET: BusinessTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessType businessType = await db.BusinessType.FindAsync(id);
            if (businessType == null)
            {
                return HttpNotFound();
            }
            return View(businessType);
        }

        // POST: BusinessTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "btypeid,btype")] BusinessType businessType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businessType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(businessType);
        }

        // GET: BusinessTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessType businessType = await db.BusinessType.FindAsync(id);
            if (businessType == null)
            {
                return HttpNotFound();
            }
            return View(businessType);
        }

        // POST: BusinessTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BusinessType businessType = await db.BusinessType.FindAsync(id);
            db.BusinessType.Remove(businessType);
            await db.SaveChangesAsync();
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
