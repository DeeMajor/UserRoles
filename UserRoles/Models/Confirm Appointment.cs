using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserRoles.Models
{
    public class Confirm_Appointment
    {
        [Key]
        public int AppId { get; set; }
        [Display(Name = "Enter Name")]
        public string name { get; set; }
        [Display(Name = "Enter Surname")]
        public string surname { get; set; }
        [Display(Name = "Enter Cellphone Number")]
        [StringLength(10)]
        public string phone { get; set; }
        [Display(Name = "Enter E-mail Address")]
        public string Address { get; set; }
        //[Display(Name ="Enter Residental Address")]
        //public string ResAd { get; set; }
        [Display(Name = "Please select a date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "Please select the time")]
        [DataType(DataType.Time)]
        public string time { get; set; }
        public virtual Agent agent { get; set; }
        public int AgentId { get; set; }
        public string status { get; set; }
    }
}