using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Header
    {
        [Key]
        public int HeaderId { get; set; }
        public string HeaderName { get; set; }
        public string HeaderType { get; set; }
    }
}