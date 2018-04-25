using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class AuthorityInformation
    {
        [Key]
        public int Auth_Id { get; set; }
        public int memberId { get; set; }
        public MemberCompanyInfo MemberCompanyInfo { get; set; }
        public string Applicant_Name { get; set; }
        public string Designation { get; set; }
        public string Father_Name { get; set; }
        public string Mother_Name { get; set; }
        public string Present_address { get; set; }
        public string Permanent_Address { get; set; }
        public string National_Id { get; set; }
        public string Mobile_No { get; set; }
        public string Tin_No { get; set; }
        public string District { get; set; }
        public string isVoter { get; set; }
        public string photo_path { get; set; }
    }
}