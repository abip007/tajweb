using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class MemberAuthViewModel
    {
        
        public int memberId { get; set; }
        public DateTime membershipDate { get; set; }
        public string Organization_Name { get; set; }
        public string Organization_Address { get; set; }
        public string Phone { get; set; }
        public string Trade_License { get; set; }
        public string Tin_Number { get; set; }
        public int AreaId { get; set; }
        public int Business_TypeId { get; set; }
        public string OrganizationType { get; set; }
        public string MemberType { get; set; }
        public Nullable<System.DateTime> InCorporationDate { get; set; }
        public string isVoter { get; set; }
        public string status { get; set; }
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
        public string photo_path { get; set; }
        public Nullable<int> Introduced_Mid { get; set; }
        public Nullable<int> int_organization_name { get; set; }
        public string Int_Autority_Name { get; set; }
        public string int_Area { get; set; }
        public string int_Adress { get; set; }
    }
}