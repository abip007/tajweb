using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class RecieveMaster
    {
        [Key]
        public int rid { get; set; }
        //public string BillNo { get; set; }

        [Display(Name ="Recieve Date")]
        public DateTime rDate { get; set; }
        public int memberId { get; set; }
        public virtual MemberCompanyInfo MemeberCompanyInfo { get; set; }
        //public Nullable<double> BillAmount { get; set; }
        public string Remarks { get; set; }
        public string  billId { get; set; }
        public string CMonth { get; set; }
        public virtual ICollection<RecieveDetail> RecieveDetail { get; set; }
        public RecieveMaster()
        {
            RecieveDetail = new List<RecieveDetail>();
        }
        public double total
        {
            get
            {
                if (RecieveDetail == null)
                    return 0;
                return RecieveDetail.Sum(i => i.Amount);
            }

        }
    }
}