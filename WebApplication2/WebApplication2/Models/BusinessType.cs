using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class BusinessType

    {
        [Key]
        public int btypeid { get; set; }
        public string btype { get; set; }
    }
}