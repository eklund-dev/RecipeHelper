using Microsoft.AspNetCore.Identity;
using RecipeHelper.Persistance.Identity.Enums;
using RecipeHelper.Persistance.Identity.Models;

namespace RecipeHelper.Persistance.Identity.Seed
{
    public static class SeedUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            foreach (var user  in GetSeedUserList())
            {
                if (await userManager.FindByEmailAsync(user.Email) is null)
                {
                    var result = await userManager.CreateAsync(user, "Abc123!");
                    if (result.Succeeded)
                    {
                        _ = user.FirstName switch
                        {
                            "User" => userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleType), RoleType.User)),
                            "Admin" => userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleType), RoleType.Admin)),
                            _ => userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleType), RoleType.User))
                        };
                    }
                }
            }

        }
        private static List<ApplicationUser> GetSeedUserList()
        {
            var userList = new List<ApplicationUser>();

            var user = new ApplicationUser
            {
                FirstName = "User",
                LastName = "Eklund",
                UserName = "CookingNovice",
                Email = "user@recipeHelper.com",
                EmailConfirmed = true,
            };

            userList.Add(user);

            var adminUser = new ApplicationUser
            {
                FirstName = "Admin",
                LastName = "Eklund",
                UserName = "TheProSaver",
                Email = "chekazure@gmail.com",
                EmailConfirmed = true
            };

            userList.Add(adminUser);

            return userList;
        }
    }
}
