using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Persistance.Data.Configurations
{
    public class RecipeCategoryConfiguration : IEntityTypeConfiguration<RecipeCategory>
    {
        public void Configure(EntityTypeBuilder<RecipeCategory> builder)
        {
            builder.ToTable("RecipeCategory");

            builder.HasKey(rc => new { rc.RecipeId, rc.CategoryId });
        }
    }
}
