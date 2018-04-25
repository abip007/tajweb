using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class MemberType
    {
        [Key]
        public int mtypeId { get; set; }
        [Display(Name ="Member Type")]
        public string MemType { get; set; }
    }
}