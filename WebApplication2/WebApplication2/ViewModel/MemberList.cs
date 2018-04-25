using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;
namespace WebApplication2.ViewModel
{
    public class MemberList
    {
        public int memberId { get; set; }
        public string mid { get; set; }
        public string Organization_Name { get; set; }
        public string Organization_Address { get; set; }
        public string Phone { get; set; }
        public string Applicant_Name { get; set; }
        public string status { get; set; }
        public double? Balance { get; set; }
    }
}