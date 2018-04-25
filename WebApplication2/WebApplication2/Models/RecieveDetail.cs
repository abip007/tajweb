using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class RecieveDetail
    {
        [Key]
        public int rdid { get; set; }
        public int rid { get; set; }
        public virtual RecieveMaster RecieveMaster { get; set; }
        //public string BillNo { get; set; }
       
        public int HeaderId { get; set; }
        public virtual Header Header { get; set; }
        //public Nullable<double> BillAmount { get; set; }
        public double Amount { get; set; }
        
    }
}