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
    public class MonthlyBillSetUPsController : Controller
    {
        private MMS db = new MMS();

        // GET: MonthlyBillSetUPs
        public async Task<ActionResult> Index()
        {
            var monthlyBillSetUPs = db.MonthlyBillSetUPs.Include(m => m.Header);
            return View(await monthlyBillSetUPs.ToListAsync());
        }

        // GET: MonthlyBillSetUPs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyBillSetUP monthlyBillSetUP = await db.MonthlyBillSetUPs.FindAsync(id);
            if (monthlyBillSetUP == null)
            {
                return HttpNotFound();
            }
            return View(monthlyBillSetUP);
        }

        // GET: MonthlyBillSetUPs/Create
        public ActionResult Create()
        {
            ViewBag.HeaderId = new SelectList(db.Headers, "HeaderId", "HeaderName");
            return View();
        }

        // POST: MonthlyBillSetUPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "mbillsetupid,HeaderId,Amount")] MonthlyBillSetUP monthlyBillSetUP)
        {
            if (ModelState.IsValid)
            {
                db.MonthlyBillSetUPs.Add(monthlyBillSetUP);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HeaderId = new SelectList(db.Headers, "HeaderId", "HeaderName", monthlyBillSetUP.HeaderId);
            return View(monthlyBillSetUP);
        }

        // GET: MonthlyBillSetUPs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyBillSetUP monthlyBillSetUP = await db.MonthlyBillSetUPs.FindAsync(id);
            if (monthlyBillSetUP == null)
            {
                return HttpNotFound();
            }
            ViewBag.HeaderId = new SelectList(db.Headers, "HeaderId", "HeaderName", monthlyBillSetUP.HeaderId);
            return View(monthlyBillSetUP);
        }

        // POST: MonthlyBillSetUPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "mbillsetupid,HeaderId,Amount")] MonthlyBillSetUP monthlyBillSetUP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthlyBillSetUP).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HeaderId = new SelectList(db.Headers, "HeaderId", "HeaderName", monthlyBillSetUP.HeaderId);
            return View(monthlyBillSetUP);
        }

        // GET: MonthlyBillSetUPs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyBillSetUP monthlyBillSetUP = await db.MonthlyBillSetUPs.FindAsync(id);
            if (monthlyBillSetUP == null)
            {
                return HttpNotFound();
            }
            return View(monthlyBillSetUP);
        }

        // POST: MonthlyBillSetUPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MonthlyBillSetUP monthlyBillSetUP = await db.MonthlyBillSetUPs.FindAsync(id);
            db.MonthlyBillSetUPs.Remove(monthlyBillSetUP);
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
