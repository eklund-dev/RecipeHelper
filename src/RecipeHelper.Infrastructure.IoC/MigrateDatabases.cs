using Microsoft.AspNetCore.Builder;
using RecipeHelper.Persistance.Data;
using RecipeHelper.Persistance.Identity;

namespace RecipeHelper.Infrastructure.IoC
{
    public static class MigrateDatabases
    {
        public static async Task<WebApplication> MigrateProjectDatabases(this WebApplication webApp)
        {
            await webApp.MigrateRecipeHelperDatabase();
            await webApp.MigrateRecipeHelperIdentityDatabase();

            return webApp;
        }
    }
}
