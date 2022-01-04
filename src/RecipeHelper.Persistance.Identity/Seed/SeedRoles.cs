using Microsoft.AspNetCore.Identity;
using RecipeHelper.Persistance.Identity.Enums;

namespace RecipeHelper.Persistance.Identity.Seed
{
    public static class SeedRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            var roleList = new List<IdentityRole>();

            if (await roleManager.FindByNameAsync(Enum.GetName(typeof(RoleType), RoleType.User)) is null)
            {
                var userRole = new IdentityRole
                {
                    Name = Enum.GetName(typeof(RoleType), RoleType.User)
                };

                roleList.Add(userRole);
            }

            if (await roleManager.FindByNameAsync(Enum.GetName(typeof(RoleType), RoleType.Admin)) is null)
            {
                var adminRole = new IdentityRole
                {
                    Name = Enum.GetName(typeof(RoleType), RoleType.Admin)
                };

                roleList.Add(adminRole);
            }

            foreach (var role in roleList)
            {
                _ = await roleManager.CreateAsync(role);
            }
        }
    }
}
