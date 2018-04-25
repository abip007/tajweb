using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class MonthlyBillSetUP
    {
        [Key]
        public int mbillsetupid { get; set; }
        public int HeaderId { get; set; }
        public virtual Header Header { get; set; }
        public double Amount { get; set; }
    }
}