using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Persistance.Data.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipe");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            builder.Property(x => x.Description).HasMaxLength(300);
            builder.Property(x => x.Duration).IsRequired();

            builder.Property(x => x.Difficulty).IsRequired();
            builder.Property(x => x.TypeOfMeal).IsRequired();
            builder.Property(x => x.TypeOfOccasion).IsRequired();

            builder.Property(x => x.Instructions)
                .IsRequired()
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v));

            builder.Property(x => x.FoodTypeId).IsRequired();
            builder.HasOne(x => x.FoodType)
                .WithMany(x => x.Recipes)
                .HasForeignKey(x => x.FoodTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
