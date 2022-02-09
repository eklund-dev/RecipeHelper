using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Persistance.Data.Configurations
{
    public class FavoriteRecipeConfiguration : IEntityTypeConfiguration<FavoriteRecipe>
    {
        public void Configure(EntityTypeBuilder<FavoriteRecipe> builder)
        {
            builder.ToTable("FavoriteRecipe");

            builder.HasKey(fr => new { fr.RecipeId, fr.RecipeUserId });

            builder.HasOne(fr => fr.Recipe)
                .WithMany(r => r.FavoriteRecipes)
                .HasForeignKey(fr => fr.RecipeId);

            builder.HasOne(fr => fr.RecipeUser)
                .WithMany(ru => ru.FavoriteRecipes)
                .HasForeignKey(fr => fr.RecipeUserId);

        }
    }
}
