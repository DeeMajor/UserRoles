using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserRoles.Models
{
    public class Images
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BuildDataID { get; set; }
        [Required]
        [Display(Name = "Building Name :")]
        public string BuildingName { get; set; }
        [Required]
        [Display(Name = "Building Address :")]
        public string BuildingAddress { get; set; }
        [Required]
        [Display(Name = "City :")]
        public string City { get; set; }
        [Required]
        public int ZipCode { get; set; }
        [Required]
        [Display(Name = "Building Type :")]
        public string BuildType { get; set; }
        [Required]
        [Display(Name = "Number of Flats :")]
        [Range(10, 300)]
        public Nullable<int> NumberFlat { get; set; }
        [Required]
        [Display(Name = "Building Description :")]
        public string FlatDescription { get; set; }
        [Required]
        [Display(Name = "Flat Price :")]
        public double FlatPrice { get; set; }

        [Display(Name = "Image")]
        public byte[] image { get; set; }

        [Required]
        [Display(Name = "Image Name")]
        public string Image_Name { get; set; }
        public string Status { get; set; }
        public string LandEmail { get; set; }
    }
}