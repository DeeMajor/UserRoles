using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserRoles.Models
{
    public class Agent
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]E:\UserRoles\UserRoles\Migrations\
        public int AgentId { get; set; }
        [Display(Name = "Enter agent name")]
        public string AgentName { get; set; }
        public string describtion { get; set; }

        public byte[] image { get; set; }

        public ICollection<Appointment> appointment { get; set; }



    }
}