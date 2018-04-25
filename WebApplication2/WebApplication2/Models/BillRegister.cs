using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class BillRegister
    {
        [Key]
        public int BillID { get; set; }
        //public string BillNo { get; set; }
        //[DataType(DataType.Date)]
       // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BillDate { get; set; }
        public int memberId { get; set; }
        public virtual MemberCompanyInfo MemeberCompanyInfo { get; set; }
        //public Nullable<double> BillAmount { get; set; }
        public string Remarks { get; set; }
        public string paid { get; set; }
        public virtual ICollection<Billdetail> Billdetail { get; set; }
        public BillRegister()
        {
            Billdetail = new List<Billdetail>();
        }
        public double total
        {
            get
            {
                if (Billdetail == null)
                    return 0;
                return Billdetail.Sum(i => i.Amount);
            }

        }
    }
}