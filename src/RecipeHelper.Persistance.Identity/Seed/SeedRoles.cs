using Microsoft.AspNetCore.Identity;
using RecipeHelper.Persistance.Identity.Enums;
using RecipeHelper.Persistance.Identity.Models;

namespace RecipeHelper.Persistance.Identity.Seed
{
    public static class SeedRoles
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
        {
            var roleList = new List<ApplicationRole>();

            if (await roleManager.FindByNameAsync(Enum.GetName(typeof(RoleType), RoleType.User)) is null)
            {
                var userRole = new ApplicationRole
                {
                    Name = Enum.GetName(typeof(RoleType), RoleType.User)
                };

                roleList.Add(userRole);
            }

            if (await roleManager.FindByNameAsync(Enum.GetName(typeof(RoleType), RoleType.Admin)) is null)
            {
                var adminRole = new ApplicationRole
                {
                    Name = Enum.GetName(typeof(RoleType), RoleType.Admin)
                };

                roleList.Add(adminRole);
            }

            if (await roleManager.FindByNameAsync(Enum.GetName(typeof(RoleType), RoleType.Owner)) is null)
            {
                var ownerRole = new ApplicationRole
                {
                    Name = Enum.GetName(typeof(RoleType), RoleType.Owner)
                };

                roleList.Add(ownerRole);
            }

            foreach (var role in roleList)
            {
                _ = await roleManager.CreateAsync(role);
            }
        }
    }
}
