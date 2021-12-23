using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeHelper.Persistance.Identity.Models;

namespace RecipeHelper.Persistance.Identity.Context
{
    public class RecipeHelperIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public RecipeHelperIdentityDbContext(DbContextOptions<RecipeHelperIdentityDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<RefreshToken>? RefreshTokens { get; set; }
    }
}
