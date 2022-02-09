using Microsoft.EntityFrameworkCore;
using RecipeHelper.Domain.Base;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Persistance.Data.Context
{
    public class RecipeHelperDbContext : DbContext
    {
        public RecipeHelperDbContext(DbContextOptions<RecipeHelperDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe>? Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient>? Ingredients { get; set; }
        public DbSet<RecipeUser>? RecipeUsers { get; set; }
        public DbSet<FoodType>? FoodTypes { get; set; }     
        public DbSet<FavoriteRecipe>? FavoriteRecipes { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecipeHelperDbContext).Assembly);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
