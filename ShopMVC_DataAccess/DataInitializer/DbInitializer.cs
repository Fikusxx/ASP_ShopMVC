using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopMVC_Models;
using ShopMVC_Utility;


namespace ShopMVC_DataAccess
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbInitializer(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void Init()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("INIT");
            try
            {
                if (db.Database.GetPendingMigrations().Count() > 0)
                {
                    db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            if (roleManager.RoleExistsAsync(WebConstants.AdminRole).GetAwaiter().GetResult() == false)
            {
                roleManager.CreateAsync(new IdentityRole() { Name = WebConstants.AdminRole }).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole() { Name = WebConstants.CustomerRole }).GetAwaiter().GetResult();
            }

            var adminEmail = "admin@gmail.com";

            userManager.CreateAsync(
                user: new ApplicationUser()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "Admin"
                },
                password: "Abcd123!").GetAwaiter().GetResult();

            var user = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
            userManager.AddToRoleAsync(user, WebConstants.AdminRole).GetAwaiter().GetResult();
        }
    }
}
