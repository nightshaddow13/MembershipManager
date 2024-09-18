using Microsoft.AspNetCore.Identity;
using MembershipManager.Data;

namespace MembershipManager;

public class UserManagerHelper
{
    public static async Task CreateDefaultAdminUser(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var adminEmail = configuration["AdminUser:Email"] ?? string.Empty;
        var adminPassword = configuration["AdminUser:Password"] ?? "superSecretPassword";

        // Check if the admin role exists
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Check if the admin user exists
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            var newAdminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail
            };
            var createAdminResult = await userManager.CreateAsync(newAdminUser, adminPassword);
            if (createAdminResult.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdminUser, "Admin");
            }
        }
    }
}
