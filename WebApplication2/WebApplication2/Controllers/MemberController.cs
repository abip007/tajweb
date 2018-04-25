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
using PagedList.Mvc;
using PagedList;

namespace WebApplication2.Controllers
{
    public class MemberController : Controller
    {
        private MMS db = new MMS();

        // GET: Member
        public ActionResult Index(int? page, string MemberId = null, string AppName = null, string orgName = null,string currentMemerid = "",string currentAppName="",string CurorgName="")
        {

             if(MemberId != null)
                {
                page = 1;
                }
                else
                {
                MemberId = currentMemerid;
                }
            if (AppName != null)
            {
                page = 1;
            }
            else
            {
                AppName = currentAppName;
            }
            if (orgName != null)
            {
                page = 1;
            }
            else
            {
                orgName = CurorgName;
            }
            ViewBag.CurorgName = orgName;
            ViewBag.currentAppName = AppName;
            ViewBag.currentMemerid = MemberId;
            var memberCompanyInfo = db.MemberCompanyInfo.Include(m => m.Area).Include(m => m.BusinessType);
            if (!String.IsNullOrEmpty(MemberId))
            {
                memberCompanyInfo = memberCompanyInfo.Where(s => s.mid == MemberId);
            }
            if (!String.IsNullOrEmpty(AppName))
            {
                memberCompanyInfo = memberCompanyInfo.Where(s => s.Applicant_Name.Contains(AppName));
            }
            if (!String.IsNullOrEmpty(orgName))
            {
                memberCompanyInfo = memberCompanyInfo.Where(s => s.Organization_Name.Contains(orgName));
            }
            memberCompanyInfo = memberCompanyInfo.OrderBy(m => m.memberId);

            int pageNumber = (page ?? 1);
            var perpagemember = memberCompanyInfo.ToPagedList(pageNumber, 20);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_members", perpagemember);
            }
            return View(perpagemember);
        }

        public ActionResult CheckExistingMemberId(string memberId)
        {
            bool ifMemberid = false;
            try
            {
                ifMemberid = memberId.Equals(db.MemberCompanyInfo.Where(s => s.mid == memberId)) ? true : false;
                return Json(!ifMemberid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Member/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCompanyInfo memberCompanyInfo = await db.MemberCompanyInfo.FindAsync(id);
            if (memberCompanyInfo == null)
            {
                return HttpNotFound();
            }
            //var TotalBalance = from bs in db.BillRegisters
            //                   join bd in db.Billdetail on bs.BillID equals bd.BillID
            //                  where bs.memberId == id && bs.paid == "N"
            //                  select new
            //                {
            //                  total =db.Billdetail.Sum(s=>s.Amount)
            //              };
            //double tot = TotalBalance.Sum(i => i.total);
            var TotalBill = db.BillRegisters.Where(a => a.memberId == id).ToList();
            var TotalRecive = db.RecieveMasters.Where(a => a.memberId == id).ToList();
            double TotalBalance = TotalBill.Sum(i => i.total)-TotalRecive.Sum(i=>i.total);
            ViewBag.Balance = TotalBalance;
            //ViewBag.nMonth = TotalBalance.Count();
            return View(memberCompanyInfo);
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            ViewBag.AreaId = new SelectList(db.Area, "AreaId", "Area_Name");
            ViewBag.btypeid = new SelectList(db.BusinessType, "btypeid", "btype");
            ViewBag.orgId = new SelectList(db.OrganizationTypes, "orgId", "OrgType");
            ViewBag.mtypeId = new SelectList(db.MemberTypes, "mtypeId", "MemType");
            ViewBag.disid = new SelectList(db.Districts, "disid", "District_Name");
            return View();
        }

        // POST: Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "mid,memberId,membershipDate,Organization_Name,Organization_Address,Phone,Trade_License,Tin_Number,AreaId,btypeid,orgId,mtypeId,InCorporationDate,isVoter,status,Applicant_Name,Designation,Father_Name,Mother_Name,Present_address,Permanent_Address,National_Id,Mobile_No,Tin_No")] MemberCompanyInfo memberCompanyInfo, HttpPostedFileBase file,FormCollection frm)
        {
            if (ModelState.IsValid)
            {

                db.MemberCompanyInfo.Add(memberCompanyInfo);
                await db.SaveChangesAsync();
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/MemberImg/"), memberCompanyInfo.mid.ToString() + ".jpg");
                    //memberCompanyInfo.photo_path = path;
                    // file is uploaded
                    file.SaveAs(path);

                    // save the image path path to the database or you can send image 
                    // directly to database
                    // in-case if you want to store byte[] ie. for DB


                }
                string int_mid = frm["int_mid"].ToString();
                if(!string.IsNullOrEmpty(int_mid))
                {
                    Introducer intd = new Introducer();
                    intd.REFID = memberCompanyInfo.mid;
                    intd.MEMBERID = int_mid;
                    db.Introducer.Add(intd);
                    db.SaveChanges();
                }
                //return RedirectToAction("Index");
            }

            ViewBag.AreaId = new SelectList(db.Area, "AreaId", "Area_Name", memberCompanyInfo.AreaId);
            ViewBag.btypeid = new SelectList(db.BusinessType, "btypeid", "btype", memberCompanyInfo.btypeid);
            return View(memberCompanyInfo);
        }
        public ActionResult GetRecordById(string memberid)
        {
            var memberCompanyInfo = db.MemberCompanyInfo.Where(s => s.mid == memberid)
                .Select(s => new
                {
                    memberid=s.memberId,
                    orgname=s.Organization_Name,
                    appname=s.Applicant_Name,
                    addr=s.Organization_Address
                }
                );


            
            if (memberCompanyInfo == null)
            {
                //return new HttpNotFoundResult();
                var result = new
                {
                    Organization_Name = "",
                    Applicant_Name = "",
                    Adress = "",
                    memid = "",
                    bill = 0


                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int memid = 0;
                string orgname = "";
                string appname = "";
                string addr = "";
                foreach (var st in memberCompanyInfo)
                {
                    memid = st.memberid;
                    orgname = st.orgname;
                    appname = st.appname;
                    addr = st.addr;

                }
                //var bill = db.BillRegisters.Where(s => s.memberId == memid && s.paid == "N")
                //    .Select(s => s.BillID).ToList();
                var TotalBill = db.BillRegisters.Where(a => a.MemeberCompanyInfo.mid == memberid).ToList();
                var TotalRecive = db.RecieveMasters.Where(a=>a.MemeberCompanyInfo.mid == memberid).ToList();
                double TotalBalance = TotalBill.Sum(i => i.total) - TotalRecive.Sum(i => i.total);
                var result = new
                {
                    Organization_Name = orgname,
                    Applicant_Name = appname,
                    Adress = addr,
                    memid = memid,
                    bill = TotalBalance


                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            
        }

        // GET: Member/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCompanyInfo memberCompanyInfo = await db.MemberCompanyInfo.FindAsync(id);
            if (memberCompanyInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaId = new SelectList(db.Area, "AreaId", "Area_Name", memberCompanyInfo.AreaId);
            ViewBag.btypeid = new SelectList(db.BusinessType, "btypeid", "btype", memberCompanyInfo.btypeid);
            ViewBag.orgId = new SelectList(db.OrganizationTypes, "orgId", "OrgType",memberCompanyInfo.orgId);
            ViewBag.mtypeId = new SelectList(db.MemberTypes, "mtypeId", "MemType",memberCompanyInfo.mtypeId);
            ViewBag.disid = new SelectList(db.Districts, "disid", "District_Name", "disid",memberCompanyInfo.disid);
            //string intrid = db.Introducer.Where(s => s.REFID == memberCompanyInfo.memberId).FirstOrDefault().ToString();
            string intrid = null;
            intrid = db.Introducer
                          .Where(s => s.REFID == memberCompanyInfo.mid)
                          .Select(s => s.MEMBERID).FirstOrDefault();






            ViewBag.Introducer = db.MemberCompanyInfo.Where(s => s.mid == intrid).SingleOrDefault();

            return View(memberCompanyInfo);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "memberId,membershipDate,Organization_Name,Organization_Address,Phone,Trade_License,Tin_Number,AreaId,btypeid,orgId,mtypeId,InCorporationDate,isVoter,status,Applicant_Name,Designation,Father_Name,Mother_Name,Present_address,Permanent_Address,National_Id,Mobile_No,Tin_No,disid,mid")] MemberCompanyInfo memberCompanyInfo, HttpPostedFileBase file,FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberCompanyInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/MemberImg/"), memberCompanyInfo.mid.ToString() + ".jpg");
                    //memberCompanyInfo.photo_path = path;
                    // file is uploaded
                    file.SaveAs(path);

                    // save the image path path to the database or you can send image 
                    // directly to database
                    // in-case if you want to store byte[] ie. for DB


                }
                string int_mid = frm["int_mid"].ToString();
                if (!string.IsNullOrEmpty(int_mid))
                {
                    var intr_id = db.Introducer.Where(s => s.REFID == memberCompanyInfo.mid).Count();
                    if (intr_id==0)
                    {

                        Introducer intd = new Introducer();
                        intd.REFID = memberCompanyInfo.mid;
                        intd.MEMBERID = int_mid;
                        db.Introducer.Add(intd);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                //Response.Write(errors.ToString());
                // Breakpoint, Log or examine the list with Exceptions.

            }
            ViewBag.AreaId = new SelectList(db.Area, "AreaId", "Area_Name", memberCompanyInfo.AreaId);
            ViewBag.btypeid = new SelectList(db.BusinessType, "btypeid", "btype", memberCompanyInfo.btypeid);
            ViewBag.orgId = new SelectList(db.OrganizationTypes, "orgId", "OrgType", memberCompanyInfo.orgId);
            ViewBag.mtypeId = new SelectList(db.MemberTypes, "mtypeId", "MemType", memberCompanyInfo.mtypeId);
            ViewBag.disid = new SelectList(db.Districts, "disid", "District_Name", "disid", memberCompanyInfo.disid);
            return View(memberCompanyInfo);
        }

        // GET: Member/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCompanyInfo memberCompanyInfo = await db.MemberCompanyInfo.FindAsync(id);
            if (memberCompanyInfo == null)
            {
                return HttpNotFound();
            }
            return View(memberCompanyInfo);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MemberCompanyInfo memberCompanyInfo = await db.MemberCompanyInfo.FindAsync(id);
            db.MemberCompanyInfo.Remove(memberCompanyInfo);
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
