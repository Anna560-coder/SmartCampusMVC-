using Microsoft.AspNetCore.Identity;
using SmartCampusMVC.Data;
using SmartCampusMVC.Models;
using System.Globalization;

namespace SmartCampusMVC.Services
{
    public class SeedService
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

            try
            {
                //Ensure the database is ready
                logger.LogInformation("Ensuring the database is created.");
                await context.Database.EnsureCreatedAsync();

                //Add roles
                logger.LogInformation("Seeding roles.");
                await AddRoleAsync(roleManager, "Student");
                await AddRoleAsync(roleManager, "Lecture");
                await AddRoleAsync(roleManager, "Technician");

                //Add Technician user
                logger.LogInformation("Seeding technician user.");
                var technicianEmail = "technician@gmail.com";
                if (await userManager.FindByEmailAsync(technicianEmail) == null) 
                {
                    var technicianUser = new Users
                    {
                        FullName = "Technician",
                        UserName = technicianEmail,
                        NormalizedUserName = technicianEmail.ToUpper(),
                        Email = technicianEmail,
                        NormalizedEmail = technicianEmail.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Faculty = "IT",
                        StudentNumber = 0
                    };

                    var result = await userManager.CreateAsync(technicianUser, "Tech@1234");
                    if(result.Succeeded)
                    {
                        logger.LogInformation("Assigning Technician role to the technician user.");
                        await userManager.AddToRoleAsync(technicianUser, "Technician");
                    }
                    else
                    {
                        logger.LogError("Failed to create technician user: {Error}", string.Join(" ", result.Errors.Select(e => e.Description)));
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured whule seeding the database.");
            }
        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception($"failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
