﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeHelper.Persistance.Data.Context;

#nullable disable

namespace RecipeHelper.Persistance.Data.Migrations
{
    [DbContext(typeof(RecipeHelperDbContext))]
    partial class RecipeHelperDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RecipeHelper.Domain.Entities.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
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

                    b.HasKey("Id");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e8d46365-470b-4d44-a126-b4afad4cdf1c"),
                            CreatedBy = "Seed",
                            CreatedDate = new DateTime(2021, 12, 26, 11, 10, 16, 882, DateTimeKind.Utc).AddTicks(6838),
                            Description = "n/a",
                            Difficulty = 1,
                            Name = "Kyckling ris & Curry"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
