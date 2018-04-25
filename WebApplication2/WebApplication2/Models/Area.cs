using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Area
    {
        [Key]
        public int AreaId { get; set; }
        public string Area_Name { get; set; }
    }
}