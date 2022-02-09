using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Persistance.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(40);

            builder.HasData(Seed());
        }

        private static List<Category> Seed()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Simple and tasty",
                    CreatedBy = "Seed",
                    CreatedDate = DateTime.UtcNow
                }
            };
        }
    }
}
