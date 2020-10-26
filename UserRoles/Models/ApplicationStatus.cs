using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserRoles.Models
{
    public class ApplicationStatus
    {
        [Key]
        public int StatusID { get; set; }
        [Display(Name = "Application Status")]
        public string Application_status { get; set; }

    }
}