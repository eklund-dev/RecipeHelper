using Microsoft.AspNetCore.Identity;
using RecipeHelper.Persistance.Identity.Enums;
using RecipeHelper.Persistance.Identity.Models;

namespace RecipeHelper.Persistance.Identity.Seed
{
    public static class SeedUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            foreach (var user in GetSeedUserList())
            {
                if (await userManager.FindByEmailAsync(user.Email) is null)
                {
                    var result = await userManager.CreateAsync(user, "Abc123!");
                    if (result.Succeeded)
                    {
                        switch (user.FirstName)
                        {
                            case "User":
                                await AddRegularUser(userManager, user);
                                break;
                            case "Admin":
                                await AddAdminUser(userManager, user);
                                break;
                            case "Owner":
                                await AddOwnerUser(userManager, user);
                                break;
                            default:
                                await AddRegularUser(userManager, user);
                                break;
                        }
                    }
                }
            }
        }

        private static async Task AddRegularUser(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("admin.view", "false"));
            await userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleType), RoleType.User));

        }
        private static async Task AddAdminUser(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("admin.view", "true"));
            await userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleType), RoleType.Admin));
        }
        private static async Task AddOwnerUser(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("admin.view", "true"));
            await userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleType), RoleType.Owner));
        }
        private static List<ApplicationUser> GetSeedUserList()
        {
            return new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    FirstName = "User",
                    LastName = "Eklund",
                    UserName = "CookingNovice",
                    Email = "user@recipeHelper.com",
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    FirstName = "Admin",
                    LastName = "Eklund",
                    UserName = "RecipeGuru",
                    Email = "admin@recipehelper.com",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    FirstName = "Owner",
                    LastName = "Eklund",
                    UserName = "MrInfinity",
                    Email = "chekazure@gmail.com",
                    EmailConfirmed = true
                }
            };
        }
    }
}
