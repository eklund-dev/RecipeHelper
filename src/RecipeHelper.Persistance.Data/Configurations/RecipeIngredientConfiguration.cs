using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Persistance.Data.Configurations
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.ToTable("RecipeIngredient");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Recipe)
                .WithMany(x => x.RecipeIngredients);
        }
    }

}
