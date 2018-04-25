using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class BillVm
    {
        [DataType(DataType.Date)]
        public DateTime billdate { get; set; }
       public int memberid { get; set; }
       public string remarks { get; set; } 
        public List<BillDetailVm> BillDetail { get; set; }
        public class BillDetailVm
        {
            public int headerid { get; set; }
            public string Header { get; set; }
            public double amount { get; set; }

        }
    }
    
}