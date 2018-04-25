using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class OrganizationType
    {
        [Key]
        public int orgId { get; set; }
        [Display(Name = "Organization Type")]
        public string OrgType { get; set; }
    }
}