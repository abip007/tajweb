using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Billdetail
    {
        [Key]
        public int lineId { get; set; }
        public int BillID { get; set; }
        public virtual BillRegister BillRegister { get; set; }
        //public string BillNo { get; set; }
       
        public int HeaderId { get; set; }
        public virtual Header Header { get; set; }
        //public Nullable<double> BillAmount { get; set; }
        public double Amount { get; set; }
        
    }
}