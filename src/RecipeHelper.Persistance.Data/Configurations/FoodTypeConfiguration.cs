using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Persistance.Data.Configurations
{
    public class FoodTypeConfiguration : IEntityTypeConfiguration<FoodType>
    {
        public void Configure(EntityTypeBuilder<FoodType> builder)
        {
            builder.ToTable("FoodType");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(30)
                .IsRequired();

            builder.HasData(Seed());
        }

        private static List<FoodType> Seed()
        {
            return new()
            {
                new FoodType
                {
                    Id = Guid.NewGuid(),
                    Name = "Meat",
                    CreatedBy = "Seed",
                    CreatedDate = DateTime.UtcNow
                },
                new FoodType
                {
                    Id = Guid.NewGuid(),
                    Name = "Fish",
                    CreatedBy = "Seed",
                    CreatedDate = DateTime.UtcNow
                },
                new FoodType
                {
                    Id = Guid.NewGuid(),
                    Name = "Chicken",
                    CreatedBy = "Seed",
                    CreatedDate = DateTime.UtcNow
                },
                new FoodType
                {
                    Id = Guid.NewGuid(),
                    Name = "Vegatarian",
                    CreatedBy = "Seed",
                    CreatedDate = DateTime.UtcNow
                },
                new FoodType
                {
                    Id = Guid.NewGuid(),
                    Name = "Vegan",
                    CreatedBy = "Seed",
                    CreatedDate = DateTime.UtcNow
                }
            };
        }
    }
}
