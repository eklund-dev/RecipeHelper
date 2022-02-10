using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Persistance.Data.Configurations
{
    public class IngreditentConfiguration : IEntityTypeConfiguration<RecipeUser>
    {
        public void Configure(EntityTypeBuilder<RecipeUser> builder)
        {
            builder.ToTable("RecipeUser");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(30);
        }     
    }
}
