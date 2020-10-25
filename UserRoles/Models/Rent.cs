using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace UserRoles.Models
{
    public class Rent : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Rental Id")]
        public int RentalId { get; set; }
        public int BuildDataID { get; set; }
        public virtual AdvertBuildings AdvertBuildings { get; set; }

        [Display(Name = "Check-In Date")]
        [DataType(DataType.Date)]
        public DateTime Check_In { get; set; }
        [Display(Name = "Check-Out Date")]
        [DataType(DataType.Date)]
        public DateTime Check_Out { get; set; }
        [Display(Name = "Number of Months")]
        public int NumMonths { get; set; }
        [Display(Name = "Room Price")]
        public double RoomPrice { get; set; }
        [Display(Name = "Total Price")]
        public double TotalPrice { get; set; }
        [Display(Name = "Monthly Installments")]
        public double Instalments { get; set; }
        public string FlatDescription { get; set; }
        public string FlatType { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();

        public double GetRoomPrice()
        {
            var price = (from i in db.AdvertBuilding
                         where i.BuildDataID == BuildDataID
                         select i.FlatPrice).FirstOrDefault();
            return price;
        }
        public string GetBuildType()
        {
            var type = (from i in db.AdvertBuilding
                        where i.BuildDataID == BuildDataID
                        select i.BuildType).FirstOrDefault();
            return type;
        }
        public string GetFlatDescription()
        {
            var type = (from i in db.AdvertBuilding
                        where i.BuildDataID == BuildDataID
                        select i.FlatDescription).FirstOrDefault();
            return type;
        }

        public Int32 GetNumberMonths()
        {
            return (((Check_Out.Date - Check_In.Date).Days) / 30);
        }

        public double Calc_Total()
        {
            TotalPrice = GetNumberMonths() * (-GetRoomPrice());
            return TotalPrice;
        }

        public double Calc_Installment()
        {
            Instalments = -Calc_Total() / GetNumberMonths();
            return Instalments;
        }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (Check_Out < Check_In)
            {
                yield return new ValidationResult("Check-Out Date Must be greater than Check-In Date");
            }
            if (Check_In <= DateTime.Now)
            {
                yield return new ValidationResult("The Check-In Date must not be less than todays date");
            }
        }
    }
}