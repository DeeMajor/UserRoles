using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;

namespace UserRoles.Models
{
    public class Application
    {
        [Key]
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

        public int StatusID { get; set; }
        public virtual ApplicationStatus ApplicationStatus { get; set; }
        //adding client foreign key. nakhona I'm taking a short cut. You can redo it ngendlela yakhona. We
        //need to know who's application is this

        public string clientId { get; set; }
    }
}