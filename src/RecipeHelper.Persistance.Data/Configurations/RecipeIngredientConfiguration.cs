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

            builder.HasKey(rc => new { rc.RecipeId, rc.IngredientId });

            builder.Property(x => x.IngredientAmountBase).IsRequired();
            builder.Property(x => x.NumberOfPortionsBase).IsRequired();
        }
    }

}
