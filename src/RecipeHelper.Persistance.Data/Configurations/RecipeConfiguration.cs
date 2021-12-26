using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Domain.Enum;

namespace RecipeHelper.Persistance.Data.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Difficulty).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(300);

            builder.HasMany(x => x.Users).WithMany(x => x.FavoriteRecipes);

            builder.HasData(SeedData());
        }

        private static Recipe[] SeedData()
        {
            return new[]
            {
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Name = "Kyckling ris & Curry",
                    Description = "n/a",
                    Difficulty = Difficulty.Easy,
                    CreatedBy = "Seed",
                    CreatedDate = DateTime.UtcNow,
                }
            };
        }
    }
}
