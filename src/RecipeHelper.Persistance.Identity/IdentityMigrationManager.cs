using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RecipeHelper.Persistance.Identity.Context;
using RecipeHelper.Persistance.Identity.Models;
using RecipeHelper.Persistance.Identity.Seed;

namespace RecipeHelper.Persistance.Identity
{
    public static class IdentityMigrationManager
    {
        public static async Task<WebApplication> MigrateRecipeHelperIdentityDatabase(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            using (var appIdentityContext = scope.ServiceProvider.GetRequiredService<RecipeHelperIdentityDbContext>())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("IdentityMigrationManager");

                try
                {
                    await appIdentityContext.Database.MigrateAsync();
                    var identityContext = services.GetRequiredService<RecipeHelperIdentityDbContext>();
                    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    var roleStore = new RoleStore<ApplicationRole>(identityContext);
                    var listofRoles = roleManager.Roles.ToList();

                    await SeedRoles.SeedAsync(roleManager);
                    await SeedUsers.SeedAsync(userManager);

                    logger.LogInformation("Identity Database Migrated successfully.");


                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex, "Migrating IdentitySeed crashed unexpectedly");
                }
            }

            return webApp;
        }
    }
}
