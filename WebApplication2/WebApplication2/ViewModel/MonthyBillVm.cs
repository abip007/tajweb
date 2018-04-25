using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.ViewModel
{
    public class MonthyBillVm
    {
        public DateTime billdate { get; set; }
        public string remarks { get; set; }
        public List<MonthlyBillSetUP> MonthlySetup { get; set; }
    }
}