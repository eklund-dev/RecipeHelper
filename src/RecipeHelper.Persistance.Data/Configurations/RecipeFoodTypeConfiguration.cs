using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Persistance.Data.Configurations
{
    public class RecipeFoodTypeConfiguration : IEntityTypeConfiguration<RecipeFoodType>
    {
        public void Configure(EntityTypeBuilder<RecipeFoodType> builder)
        {
            builder.ToTable("RecipeFoodType");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}
