// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeHelper.Persistance.Data.Context;

#nullable disable

namespace RecipeHelper.Persistance.Data.Migrations
{
    [DbContext(typeof(RecipeHelperDbContext))]
    [Migration("20211226140307_NewTables")]
    partial class NewTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.Property<Guid>("IngredientsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RecipesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IngredientsId", "RecipesId");

                    b.HasIndex("RecipesId");

                    b.ToTable("IngredientRecipe");
                });

            modelBuilder.Entity("RecipeHelper.Domain.Entities.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastmodifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = new Guid("32187963-89d6-482f-a31e-2ca0b7868deb"),
                            CreatedBy = "Seed",
                            CreatedDate = new DateTime(2021, 12, 26, 14, 3, 6, 923, DateTimeKind.Utc).AddTicks(230),
                            Name = "Salt"
                        },
                        new
                        {
                            Id = new Guid("f22740e1-9da7-40ce-b13e-957f5ee36c2a"),
                            CreatedBy = "Seed",
                            CreatedDate = new DateTime(2021, 12, 26, 14, 3, 6, 923, DateTimeKind.Utc).AddTicks(235),
                            Name = "Peppar"
                        });
                });

            modelBuilder.Entity("RecipeHelper.Domain.Entities.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastmodifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Occasion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("850766c4-24f9-4a1e-851f-50f32f980095"),
                            Category = 0,
                            CreatedBy = "Seed",
                            CreatedDate = new DateTime(2021, 12, 26, 14, 3, 6, 923, DateTimeKind.Utc).AddTicks(1683),
                            Description = "n/a",
                            Difficulty = 1,
                            Name = "Kyckling ris & Curry",
                            Occasion = 0
                        });
                });

            modelBuilder.Entity("RecipeHelper.Domain.Entities.RecipeUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdentityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastmodifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("RecipeUsers");
                });

            modelBuilder.Entity("RecipeRecipeUser", b =>
                {
                    b.Property<Guid>("FavoriteRecipesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FavoriteRecipesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RecipeRecipeUser");
                });

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.HasOne("RecipeHelper.Domain.Entities.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeHelper.Domain.Entities.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecipeRecipeUser", b =>
                {
                    b.HasOne("RecipeHelper.Domain.Entities.Recipe", null)
                        .WithMany()
                        .HasForeignKey("FavoriteRecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeHelper.Domain.Entities.RecipeUser", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
