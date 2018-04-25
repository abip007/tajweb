using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Introducer
    {
        [Key]
        public int intid { get; set; }
        public string MEMBERID { get; set; }
        //public MemberCompanyInfo MemberInfo { get; set; }

        public string REFID { get; set; }
    }
}