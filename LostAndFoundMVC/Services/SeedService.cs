using Microsoft.AspNetCore.Identity;
using LostAndFoundMVC.Data;
using LostAndFoundMVC.Models;

namespace UserRoles.Services
{
    public class SeedService
    {
        public static async Task SeedAdmin(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<Users>>();

            string role = "Admin";
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            string email = "admin@site.com";
            string password = "Admin123!";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var admin = new Users { UserName = email, Email = email };
                await userManager.CreateAsync(admin, password);
                await userManager.AddToRoleAsync(admin, role);
            }
        }
    }
}


