using Microsoft.AspNetCore.Identity;

namespace RecipeHelper.Persistance.Identity.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

    }
}
