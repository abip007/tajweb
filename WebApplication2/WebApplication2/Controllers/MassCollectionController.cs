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
    public class MassCollectionController : Controller
    {
        private MMS db = new MMS();
        // GET: MassCollection
        public ActionResult Index(int? page, string MemberId = null, int BillID = 0, string sDate = null, string eDate = null, string currentMemerid = "", int currentBillID = 0, string CurrsDate = "", string curreDate = "")
        {
            if (MemberId != null)
            {
                page = 1;
            }
            else
            {
                MemberId = currentMemerid;
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


            var billRegisters = db.BillRegisters.Include(b => b.MemeberCompanyInfo).Where(s=>s.paid=="N");
            if (!String.IsNullOrEmpty(MemberId))
            {
                List<string> memid = new List<string>();
                string[] str = MemberId.Split(',');
                foreach (string s in str)
                {
                    memid.Add(s);
                }


                billRegisters = from c in billRegisters
                                where memid.Contains(c.MemeberCompanyInfo.mid)
                                select c;
            }
            if (!String.IsNullOrEmpty(sDate) && !String.IsNullOrEmpty(eDate))
            {
                DateTime dtfrom = Convert.ToDateTime(sDate);
                DateTime dtTo = Convert.ToDateTime(eDate);
                billRegisters = billRegisters.Where(s => s.BillDate >= dtfrom && s.BillDate <= dtTo);
            }


           
            billRegisters = billRegisters.OrderBy(s => s.BillID);
            int pageNumber = (page ?? 1);
            return View(billRegisters.ToPagedList(pageNumber, 100));
        }
        [HttpPost]
        public ActionResult  Mass(FormCollection frm )
        {
            string obillid = frm["BillID"];
            string odate = frm["edate"];
            string[] obill = obillid.Split(',');
            foreach (var obi in obill)
            {
                int bi = Convert.ToInt32(obi);
                var billid = db.BillRegisters.Find(bi);
                if (billid != null)

                {
                    billid.paid = "Y";
                    db.SaveChanges();

                    RecieveMaster rc = new RecieveMaster();
                    rc.memberId = billid.memberId;
                    rc.rDate =Convert.ToDateTime(odate) ;
                    rc.billId = billid.BillID.ToString();
                    //rc.total = billid.total;
                   // rc.Remarks = "Mass Received";
                    rc.CMonth = billid.BillDate.Month.ToString("MMMM");
                    db.RecieveMasters.Add(rc);
                    db.SaveChanges();
                    int rid = rc.rid;
                    var billdetail = db.Billdetail.Where(a => a.BillID == bi);
                    foreach(var bild in billdetail)
                    {
                        RecieveDetail rd = new RecieveDetail();
                        rd.rid = rid;
                        rd.HeaderId = bild.HeaderId;
                        rd.Amount = bild.Amount;
                        db.RecieveDetail.Add(rd);
                       
                    }
                    db.SaveChanges();

                }

            }
            // Response.Write(billid);
            //return View();
            return RedirectToAction("Index", "MassCollection");
        }
    }
}