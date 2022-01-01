using Microsoft.AspNetCore.Identity;

namespace RecipeHelper.Persistance.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsActive { get; set; }

        public string GetFullName() => $"{FirstName} {LastName}";
    }
}
