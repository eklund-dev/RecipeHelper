using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using RecipeHelper.Persistance.Data.Context;

namespace RecipeHelper.Persistance.Data
{
    public static class RecipeHelperMigrationManager
    {
        public static async Task<WebApplication> MigrateRecipeHelperDatabase(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            using (var recipeHelperContext = scope.ServiceProvider.GetRequiredService<RecipeHelperDbContext>())
            {
                var loggerFactory = webApp.Services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("RecipeHelperMigrationManager");

                try
                {
                    await recipeHelperContext.Database.MigrateAsync();
                    logger.LogInformation("Migrating RecipeHelperContext as an Async Operation");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Migrating RecipeHelperDb didn't work.");
                    throw new Exception("Migrating RecipeHelperDb failed", ex);
                }
            }

            return webApp;
        }
 
    }
}
