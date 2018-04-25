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
using System.Text;
using PagedList;
namespace WebApplication2.Controllers
{
    public class RecieveController : Controller
    {
        private MMS db = new MMS();

        // GET: RecieveMasters
        public ActionResult Index(int? page, string MemberId = null, int rid = 0, string sDate = null, string eDate = null, string currentMemerid = "", int currentBillID = 0, string CurrsDate = "", string curreDate = "")
        {
            if (MemberId != null)
            {
                page = 1;
            }
            else
            {
                MemberId = currentMemerid;
            }
            if (rid != 0)
            {
                page = 1;
            }
            else
            {
                rid = currentBillID;
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
            ViewBag.currentBillID = rid;
            ViewBag.currentMemerid = MemberId;
            var recieveMasters = db.RecieveMasters.Include(r => r.MemeberCompanyInfo);
            if (!String.IsNullOrEmpty(MemberId))
            {
                recieveMasters = recieveMasters.Where(s => s.MemeberCompanyInfo.mid == MemberId);
            }
            if (!String.IsNullOrEmpty(sDate) && !String.IsNullOrEmpty(eDate))
            {
                DateTime dtfrom = Convert.ToDateTime(sDate);
                DateTime dtTo = Convert.ToDateTime(eDate);
                recieveMasters = recieveMasters.Where(s => s.rDate >= dtfrom && s.rDate <= dtTo);
            }


            if (rid > 0)
            {
                recieveMasters = recieveMasters.Where(s => s.rid == rid);
            }
            recieveMasters = recieveMasters.OrderBy(s => s.rid);
            int pageNumber = (page ?? 1);
            return View(recieveMasters.ToPagedList(pageNumber, 30));
        }

        // GET: RecieveMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecieveMaster recieveMaster = await db.RecieveMasters.FindAsync(id);
            if (recieveMaster == null)
            {
                return HttpNotFound();
            }
            return View(recieveMaster);
        }

        // GET: RecieveMasters/Create
        public ActionResult Create()
        {
            ViewBag.HeaderId = new SelectList(db.Headers.Where(s => s.HeaderType == "Income"), "HeaderId", "HeaderName", "Select Head");
            return View();
        }

        // POST: RecieveMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "rid,rDate,memberId,Remarks,billId")] RecieveMaster recieveMaster)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.RecieveMasters.Add(recieveMaster);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.memberId = new SelectList(db.MemberCompanyInfo, "memberId", "Organization_Name", recieveMaster.memberId);
        //    return View(recieveMaster);
        //}


        [HttpPost]
        public JsonResult SaveOrder(RecieveVm O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {

                //Order order = new Order { OrderNo = O.OrderNo, OrderDate = O.OrderDate, Description = O.Description };
                string billiid = "";
                string cmonth = "";
                int t = 0;
                //foreach (var k in O.billid)
                //{
                //    if (t == 0)
                //    {
                //        billiid = k.ToString();
                //        t = 1;
                //    }
                //    else
                //        billiid += "," + k.ToString();

                //}
               // t = 0;
                foreach (var k in O.CMonth)
                {
                    if (t == 0)
                    {
                        cmonth = k.ToString();
                        t = 1;
                    }
                    else
                        cmonth += "," + k.ToString();

                }


                RecieveMaster recievMaster = new RecieveMaster { rDate = O.recivedate, Remarks = O.remarks, memberId = O.memberid, billId = billiid,CMonth=cmonth };
                db.RecieveMasters.Add(recievMaster);
                db.SaveChanges();
                foreach (var i in O.ReciveDetail)
                {
                    //
                    // i.TotalAmount = 
                    RecieveDetail recievedetail = new RecieveDetail();
                    recievedetail.rid = recievMaster.rid;
                    recievedetail.HeaderId = i.headerid;
                    recievedetail.Amount = i.amount;
                    db.RecieveDetail.Add(recievedetail);
                    db.SaveChanges();
                    Session["rid"] = recievMaster.rid;
                }
                //foreach(var j in O.billid)
                //{
                //    int bi = int.Parse(j);
                //    var billid = db.BillRegisters.Find(bi);
                //    if (billid!=null)

                //    {
                //        billid.paid = "Y";
                //        db.SaveChanges();

                //    }




                //}

                
                status = true;

            }
            else
            {
                status = false;
            }
            var TotalBill = db.BillRegisters.Where(a => a.memberId == O.memberid).ToList();
            var TotalRecive = db.RecieveMasters.Where(a => a.memberId == O.memberid).ToList();
            double TotalBalance = TotalBill.Sum(i => i.total) - TotalRecive.Sum(i => i.total);
            return new JsonResult { Data = new { status = status,balance=TotalBalance } };
        }

        // GET: RecieveMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecieveMaster recieveMaster = await db.RecieveMasters.FindAsync(id);
            if (recieveMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.memberId = new SelectList(db.MemberCompanyInfo, "memberId", "Organization_Name", recieveMaster.memberId);
            ViewBag.bill = db.BillRegisters.Where(s => s.memberId == recieveMaster.memberId && s.paid == "N")
                       .Select(s => s.BillID).ToList();
            ViewBag.HeaderId = new SelectList(db.Headers.Where(s => s.HeaderType == "Income"), "HeaderId", "HeaderName", "Select Head");
            return View(recieveMaster);
        }

        // POST: RecieveMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "rid,rDate,memberId,Remarks,billId")] RecieveMaster recieveMaster,FormCollection form)
        {
            if (ModelState.IsValid)
            {
                string obillid = form["obillid"].ToString();
                if (!String.IsNullOrEmpty(obillid))
                {
                    string[] obill = obillid.Split(',');
                    foreach (var obi in obill)
                    {
                        int bi = Convert.ToInt32(obi);
                        var billid = db.BillRegisters.Find(bi);
                        if (billid != null)

                        {
                            billid.paid = "N";
                            db.SaveChanges();

                        }

                    }
                }
                    string bllid = form["billId"].ToString();
                if (!String.IsNullOrEmpty(bllid))
                { 
                    string[] oill = bllid.Split(',');
                    foreach (var obi in oill)
                    {
                        int bi = Convert.ToInt32(obi);
                        var billd = db.BillRegisters.Find(bi);
                        if (billd != null)

                        {
                            
                            billd.paid = "Y";
                            db.SaveChanges();

                        }

                    }
                }
                recieveMaster.billId = bllid.ToString();

                db.Entry(recieveMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.memberId = new SelectList(db.MemberCompanyInfo, "memberId", "Organization_Name", recieveMaster.memberId);
            return View(recieveMaster);
        }

        // GET: RecieveMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecieveMaster recieveMaster = await db.RecieveMasters.FindAsync(id);
            if (recieveMaster == null)
            {
                return HttpNotFound();
            }
            return View(recieveMaster);
        }

        // POST: RecieveMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RecieveDetail recievedetail = await db.RecieveDetail.FindAsync(id);
            if (recievedetail != null)
            {
                db.RecieveDetail.Remove(recievedetail);
            }
            RecieveMaster recieveMaster = await db.RecieveMasters.FindAsync(id);
            string billid = recieveMaster.billId.ToString();
            if (billid != "0")
            {
                if (!String.IsNullOrEmpty(billid))

                {
                    string[] obill = billid.Split(',');
                    foreach (var obi in obill)
                    {
                        int bi = Convert.ToInt32(obi);
                        var bllid = db.BillRegisters.Find(bi);
                        if (bllid != null)

                        {
                            bllid.paid = "N";
                            db.SaveChanges();

                        }

                    }

                }
            }

            db.RecieveMasters.Remove(recieveMaster);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> DeleteLine(int id)
        {
            RecieveDetail Receivedetail = await db.RecieveDetail.FindAsync(id);
            db.RecieveDetail.Remove(Receivedetail);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit/" + Receivedetail.rid);
        }
        [HttpPost]
        public async Task<JsonResult> updateAmount(int rdid, double amount)
        {
            var record = await db.RecieveDetail.FindAsync(rdid);
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
        [HttpPost]
        public JsonResult AddLinetoRecive(int HeaderId, double Amount, int rid, string HeaderText)
        {
            bool status = false;
            int rdd = 0;

            if (ModelState.IsValid)
            {

                //Order order = new Order { OrderNo = O.OrderNo, OrderDate = O.OrderDate, Description = O.Description };

                //
                // i.TotalAmount = 
                RecieveDetail Recievedetail = new RecieveDetail();
                Recievedetail.rid = rid;
                Recievedetail.HeaderId = HeaderId;
                Recievedetail.Amount = Amount;
                db.RecieveDetail.Add(Recievedetail);
                db.SaveChanges();
                rdd = Recievedetail.rdid;

                status = true;

            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status, rdd = rdd, amount = Amount } };
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
