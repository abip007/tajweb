using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class RecieveVm
    {

        [DataType(DataType.Date)]
        public DateTime recivedate { get; set; }
        public int memberid { get; set; }
        public string remarks { get; set; }
        public string[] CMonth { get; set; }
        public List<RecieveDetailVm> ReciveDetail { get; set; }
        public string [] billid { set; get; }
        public class RecieveDetailVm
        {
            public int headerid { get; set; }
            public string Header { get; set; }
            public double amount { get; set; }

        }
    }
}