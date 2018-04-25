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
    public class MemberTypesController : Controller
    {
        private MMS db = new MMS();

        // GET: MemberTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.MemberTypes.ToListAsync());
        }

        // GET: MemberTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberType memberType = await db.MemberTypes.FindAsync(id);
            if (memberType == null)
            {
                return HttpNotFound();
            }
            return View(memberType);
        }

        // GET: MemberTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "mtypeId,MemType")] MemberType memberType)
        {
            if (ModelState.IsValid)
            {
                db.MemberTypes.Add(memberType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(memberType);
        }

        // GET: MemberTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberType memberType = await db.MemberTypes.FindAsync(id);
            if (memberType == null)
            {
                return HttpNotFound();
            }
            return View(memberType);
        }

        // POST: MemberTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "mtypeId,MemType")] MemberType memberType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(memberType);
        }

        // GET: MemberTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberType memberType = await db.MemberTypes.FindAsync(id);
            if (memberType == null)
            {
                return HttpNotFound();
            }
            return View(memberType);
        }

        // POST: MemberTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MemberType memberType = await db.MemberTypes.FindAsync(id);
            db.MemberTypes.Remove(memberType);
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
