using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Districts

    {
        [Key]
        public int disid { get; set; }
        public string District_Name { get; set; }
    }
}