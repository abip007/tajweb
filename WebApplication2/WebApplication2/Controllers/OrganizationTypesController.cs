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
    public class OrganizationTypesController : Controller
    {
        private MMS db = new MMS();

        // GET: OrganizationTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.OrganizationTypes.ToListAsync());
        }

        // GET: OrganizationTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationType organizationType = await db.OrganizationTypes.FindAsync(id);
            if (organizationType == null)
            {
                return HttpNotFound();
            }
            return View(organizationType);
        }

        // GET: OrganizationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "orgId,OrgType")] OrganizationType organizationType)
        {
            if (ModelState.IsValid)
            {
                db.OrganizationTypes.Add(organizationType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(organizationType);
        }

        // GET: OrganizationTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationType organizationType = await db.OrganizationTypes.FindAsync(id);
            if (organizationType == null)
            {
                return HttpNotFound();
            }
            return View(organizationType);
        }

        // POST: OrganizationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "orgId,OrgType")] OrganizationType organizationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizationType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(organizationType);
        }

        // GET: OrganizationTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationType organizationType = await db.OrganizationTypes.FindAsync(id);
            if (organizationType == null)
            {
                return HttpNotFound();
            }
            return View(organizationType);
        }

        // POST: OrganizationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrganizationType organizationType = await db.OrganizationTypes.FindAsync(id);
            db.OrganizationTypes.Remove(organizationType);
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
