using UserRoles.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserRoles.Startup))]
namespace UserRoles
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRole();
            CreateUser();
        }
    }
}
