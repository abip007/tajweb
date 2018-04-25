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
using WebApplication2.ViewModel;
using PagedList;

namespace WebApplication2.Controllers
{
    public class BillRegistersController : Controller
    {
        private MMS db = new MMS();

        // GET: BillRegisters
        public ActionResult Index(int? page, string MemberId = null, int BillID = 0, string sDate = null, string eDate=null, string currentMemerid = "", int currentBillID = 0, string CurrsDate = "",string curreDate="")
        {
            if (MemberId != null)
            {
                page = 1;
            }
            else
            {
                MemberId = currentMemerid;
            }
            if (BillID != 0)
            {
                page = 1;
            }
            else
            {
                BillID = currentBillID;
            }
            if (sDate != null)
            {
                page = 1;
            }
            else
            {
                sDate = CurrsDate;
            }
            if (eDate != null)
            {
                page = 1;
            }
            else
            {
                eDate = curreDate;
            }
            ViewBag.CurrsDate = sDate;
            ViewBag.curreDate = eDate;
            ViewBag.currentBillID = BillID;
            ViewBag.currentMemerid = MemberId;


            var billRegisters = db.BillRegisters.Include(b => b.MemeberCompanyInfo);
            if (!String.IsNullOrEmpty(MemberId))
            {
                billRegisters = billRegisters.Where(s => s.MemeberCompanyInfo.mid == MemberId);
            }
            if (!String.IsNullOrEmpty(sDate) && !String.IsNullOrEmpty(eDate))
            {
                DateTime dtfrom = Convert.ToDateTime(sDate);
                DateTime dtTo = Convert.ToDateTime(eDate);
                billRegisters = billRegisters.Where(s => s.BillDate >=dtfrom && s.BillDate <=dtTo);
            }


            if(BillID>0)
            {
                billRegisters = billRegisters.Where(s => s.BillID == BillID);
            }
            billRegisters = billRegisters.OrderBy(s => s.BillID);
            int pageNumber = (page ?? 1);
            return View(billRegisters.ToPagedList(pageNumber,20));
        }

        // GET: BillRegisters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillRegister billRegister = await db.BillRegisters.FindAsync(id);
            if (billRegister == null)
            {
                return HttpNotFound();
            }
            return View(billRegister);
        }

        // GET: BillRegisters/Create
        public ActionResult Create()
        {
            ViewBag.HeaderId = new SelectList(db.Headers.Where(s => s.HeaderType == "Income"), "HeaderId", "HeaderName", "Select Head");
            return View();
        }

        // POST: BillRegisters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "BillID,BillDate,memberId,Remarks,paid")] BillRegister billRegister)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.BillRegisters.Add(billRegister);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.HeaderId = new SelectList(db.Headers.Where(s => s.HeaderType == "Income"), "HeaderId", "HeaderName", "Select Head");

        //    return View(billRegister);
        //}

        [HttpPost]
        public JsonResult SaveOrder(BillVm O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {

                //Order order = new Order { OrderNo = O.OrderNo, OrderDate = O.OrderDate, Description = O.Description };
                BillRegister billregister = new BillRegister { BillDate = O.billdate, Remarks = O.remarks, memberId = O.memberid,paid="N" };
                db.BillRegisters.Add(billregister);
                db.SaveChanges();
                foreach (var i in O.BillDetail)
                    {
                    //
                    // i.TotalAmount = 
                    Billdetail billdetail = new Billdetail();
                    billdetail.BillID = billregister.BillID;
                    billdetail.HeaderId = i.headerid;
                    billdetail.Amount = i.amount;
                    db.Billdetail.Add(billdetail);
                    db.SaveChanges();
                    }
                   
                    status = true;
               
            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult AddLinetoBill(int HeaderId,double Amount,int BillID,string HeaderText)
        {
            bool status = false;
            int lineid = 0;

            if (ModelState.IsValid)
            {

                //Order order = new Order { OrderNo = O.OrderNo, OrderDate = O.OrderDate, Description = O.Description };
               
                    //
                    // i.TotalAmount = 
                    Billdetail billdetail = new Billdetail();
                    billdetail.BillID = BillID;
                    billdetail.HeaderId = HeaderId;
                    billdetail.Amount = Amount;
                    db.Billdetail.Add(billdetail);
                    db.SaveChanges();
                lineid = billdetail.lineId;

                status = true;

            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status, lineid=lineid,amount=Amount } };
        }

        public ActionResult GetUnpaidBIllById(int memberid)
        {
            BillRegister bill = db.BillRegisters.Find(memberid);
            if (bill == null)
            {
                return new HttpNotFoundResult();
            }
            var result = new
            {
                billid = bill.BillID,
                billdate = bill.BillID.ToString() + '-' + bill.BillDate


            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: BillRegisters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillRegister billRegister = await db.BillRegisters.FindAsync(id);
            if (billRegister == null)
            {
                return HttpNotFound();
            }
            ViewBag.memberId = new SelectList(db.MemberCompanyInfo, "memberId", "Organization_Name", billRegister.memberId);
            ViewBag.BillDetail = db.Billdetail.Include(p=>p.Header).Where(s => s.BillID== id).ToList();
            ViewBag.HeaderId = new SelectList(db.Headers.Where(s => s.HeaderType == "Income"), "HeaderId", "HeaderName", "Select Head");
            return View(billRegister);
        }

        // POST: BillRegisters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BillID,BillDate,memberId,Remarks,paid")] BillRegister billRegister)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billRegister).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.memberId = new SelectList(db.MemberCompanyInfo, "memberId", "Organization_Name", billRegister.memberId);
            return View(billRegister);
        }

        // GET: BillRegisters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillRegister billRegister = await db.BillRegisters.FindAsync(id);
            if (billRegister == null)
            {
                return HttpNotFound();
            }
            return View(billRegister);
        }

        // POST: BillRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BillRegister billRegister = await db.BillRegisters.FindAsync(id);
            db.BillRegisters.Remove(billRegister);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        
        public async Task<ActionResult> DeleteLine(int id)
        {
            Billdetail billdetail = await db.Billdetail.FindAsync(id);
            db.Billdetail.Remove(billdetail);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit/"+billdetail.BillID);
        }

        [HttpPost]
        public async Task<JsonResult> updateAmount(int lineid, double amount)
        {
            var record = await db.Billdetail.FindAsync(lineid);
            if (record != null)
            {
                record.Amount = amount;
                await db.SaveChangesAsync();
            }
            var result = new
            {
                ponum = record.Amount
            };
            return Json(result);
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
