using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    public class MemberCompanyInfo
    {


        [Key]
        public int memberId { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50, MinimumLength = 3)]
        [Remote("CheckExistingMemberId", "Member", ErrorMessage = "MemberId already exists!")]
        public string mid { get; set; }
        public DateTime membershipDate { get; set; }
        public string Organization_Name { get; set; }
        public string Organization_Address { get; set; }
        public string Phone { get; set; }
        public string Trade_License { get; set; }
        public string Tin_Number { get; set; }
        public string Vat_No { get; set; }
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public int btypeid { get; set; }
        public virtual BusinessType BusinessType { get; set; }
        public int orgId { get; set; }
        public virtual OrganizationType OrganizationType { get; set; }
        public int mtypeId { get; set; }
        public virtual MemberType MemberType { get; set; }
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
        public int disid { get; set; }
        public virtual Districts Districts { get; set; }
        // public string photo_path { get; set; }
        //public Nullable<int> Introduced_Mid { get; set; }
        //public string int_organization_name { get; set; }
        //public string Int_Autority_Name { get; set; }
        //public string int_Area { get; set; }
        //public string int_Adress { get; set; }
    }
}