using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MonthlyBillController : Controller
    {
        // GET: MonthlyBill
        private MMS db = new MMS();
        public ViewResult Index()
        {
            var monthsetup = db.MonthlyBillSetUPs.ToList();
            return View(monthsetup);
        }
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            int temp = 0;
           // Thread.Sleep(1000);
            if (ModelState.IsValid)
            {
                List<MemberCompanyInfo> memberinfo = db.MemberCompanyInfo.Where(s => s.status != "P").ToList();
                List<MonthlyBillSetUP> billsetup = db.MonthlyBillSetUPs.ToList();

               // List<MemberCompanyInfo> memberinfo = db.MemberCompanyInfo.SqlQuery("Select * from MemberCompanyInfo Where status='P'").ToList();
               // List<MonthlyBillSetUP> billsetup = db.MonthlyBillSetUPs.ToList();
                foreach (var member in memberinfo)
                {
                    BillRegister billregister = new BillRegister();
                    billregister.BillDate = Convert.ToDateTime(form["BillDate"]);
                    billregister.memberId = member.memberId;
                    billregister.paid = "N";
                    billregister.Remarks = form["Remarks"].ToString();
                    db.BillRegisters.Add(billregister);
                    db.SaveChanges();
                    Billdetail billdetail = new Billdetail();
                    foreach (var bs in billsetup)
                    {
                        billdetail.BillID = billregister.BillID;
                        billdetail.Amount = bs.Amount;
                        billdetail.HeaderId = bs.HeaderId;
                        db.Billdetail.Add(billdetail);
                        db.SaveChanges();
                    }

                    temp = 1;

                }
            }
            //    var result = new {


            //        res="Data Saved"

            //};
            //var result1 = new
            //{


            //    res = "Error"

            //};
            //if (temp == 1)
            //{
            //    return Json(result);
            //}
            //else
            //{
            //    return Json(result1);
            //}
            return View();
        }
    }
}