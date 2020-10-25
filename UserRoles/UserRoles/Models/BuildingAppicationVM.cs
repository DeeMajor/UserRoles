using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserRoles.Models
{
    public class BuildingAppicationVM 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BuildingAppicationVM_Id { get; set; }
        public string BuildingName { get; set; }
        public string BuildingAddress { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string BuildType { get; set; }
        public Nullable<int> NumberFlat { get; set; }
        public string FlatDescription { get; set; }
        public double FlatPrice { get; set; }
        public byte[] image { get; set; }
        public string Image_Name { get; set; }
        public decimal Amount { get; set; }






    }
}