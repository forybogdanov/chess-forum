using GameWebsite.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameWebsite.Web.Seed
{
    public static class DatabaseSeedUtilities
    {
        public static void SeedRoles(this WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                using (var GameWebsiteDbContext = serviceScope.ServiceProvider.GetRequiredService<GameWebsiteDbContext>())
                {
                    GameWebsiteDbContext.Database.Migrate();

                    if (GameWebsiteDbContext.Roles.ToList().Count == 0)
                    {
                        IdentityRole adminRole = new IdentityRole();
                        adminRole.Name = "Admin";
                        adminRole.NormalizedName = adminRole.Name.ToUpper();

                        IdentityRole userRole = new IdentityRole();
                        userRole.Name = "User";
                        userRole.NormalizedName = userRole.Name.ToUpper();

                        GameWebsiteDbContext.Add(adminRole);
                        GameWebsiteDbContext.Add(userRole);

                        GameWebsiteDbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
