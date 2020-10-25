using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace UserRoles.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100, ErrorMessage = "Your Name")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name ")]
        public string FirstName { get; set; }


        [StringLength(100, ErrorMessage = "Your Surname")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [StringLength(100, ErrorMessage = "invalid Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "User Type")]
        [DataType(DataType.Text)]
        //[RegularExpression(@"^[a-z-']+$", ErrorMessage = "Invalid name ")]
        [Display(Name = "User Type")]
        public string UserType { get; set; }



        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
       public enum Gender
    {
       Male,Female
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
       
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
    }
}