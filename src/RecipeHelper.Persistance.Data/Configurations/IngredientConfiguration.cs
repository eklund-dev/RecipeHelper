using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Persistance.Data.Configurations
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("Ingredient");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(30);

            builder.HasMany(x => x.RecipeIngredients)
                .WithOne(x => x.Ingredient)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(Seed());
        }

        private static Ingredient[] Seed()
        {
            return new[]
            {
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Salt",
                    CreatedBy = "Seed",
                    CreatedDate = DateTime.UtcNow,
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Peppar",
                    CreatedBy = "Seed",
                    CreatedDate = DateTime.UtcNow
                },
            };
        }
    }
}
