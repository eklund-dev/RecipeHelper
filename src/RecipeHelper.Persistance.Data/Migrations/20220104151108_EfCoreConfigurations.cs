using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHelper.Persistance.Data.Migrations
{
    public partial class EfCoreConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: new Guid("425498f8-c981-4796-a12c-8288762936f1"));

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: new Guid("d7b255e6-7ac1-4a3d-820a-1f1ffa78ecba"));

            migrationBuilder.DeleteData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("55921cce-0258-4ce3-9a7e-17b474c1d446"));

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Recipe",
                newName: "Duration");

            migrationBuilder.AddColumn<int>(
                name: "CourseCategoryId",
                table: "Recipe",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeFoodTypeId",
                table: "Recipe",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastmodifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeFoodType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastmodifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeFoodType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[] { new Guid("852395a4-3aa4-4cdc-91cd-f0ce6340835e"), "Seed", new DateTime(2022, 1, 4, 15, 11, 8, 365, DateTimeKind.Utc).AddTicks(3849), null, null, "Salt" });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[] { new Guid("defd89d7-65fd-4aaa-932a-06da9661bdee"), "Seed", new DateTime(2022, 1, 4, 15, 11, 8, 365, DateTimeKind.Utc).AddTicks(3853), null, null, "Peppar" });

            migrationBuilder.InsertData(
                table: "Recipe",
                columns: new[] { "Id", "CourseCategoryId", "CreatedBy", "CreatedDate", "Description", "Difficulty", "Duration", "LastModifiedDate", "LastmodifiedBy", "Name", "Occasion", "RecipeFoodTypeId" },
                values: new object[] { new Guid("318c01ed-3f7a-4b21-9934-ce793d536673"), null, "Seed", new DateTime(2022, 1, 4, 15, 11, 8, 365, DateTimeKind.Utc).AddTicks(8916), "n/a", 1, 0, null, null, "Kyckling ris & Curry", 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_RecipeFoodTypeId",
                table: "Recipe",
                column: "RecipeFoodTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_CourseCategory_RecipeFoodTypeId",
                table: "Recipe",
                column: "RecipeFoodTypeId",
                principalTable: "CourseCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_RecipeFoodType_RecipeFoodTypeId",
                table: "Recipe",
                column: "RecipeFoodTypeId",
                principalTable: "RecipeFoodType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_CourseCategory_RecipeFoodTypeId",
                table: "Recipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_RecipeFoodType_RecipeFoodTypeId",
                table: "Recipe");

            migrationBuilder.DropTable(
                name: "CourseCategory");

            migrationBuilder.DropTable(
                name: "RecipeFoodType");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_RecipeFoodTypeId",
                table: "Recipe");

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: new Guid("852395a4-3aa4-4cdc-91cd-f0ce6340835e"));

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: new Guid("defd89d7-65fd-4aaa-932a-06da9661bdee"));

            migrationBuilder.DeleteData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("318c01ed-3f7a-4b21-9934-ce793d536673"));

            migrationBuilder.DropColumn(
                name: "CourseCategoryId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "RecipeFoodTypeId",
                table: "Recipe");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Recipe",
                newName: "Category");

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[] { new Guid("425498f8-c981-4796-a12c-8288762936f1"), "Seed", new DateTime(2021, 12, 28, 8, 27, 12, 823, DateTimeKind.Utc).AddTicks(1950), null, null, "Salt" });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[] { new Guid("d7b255e6-7ac1-4a3d-820a-1f1ffa78ecba"), "Seed", new DateTime(2021, 12, 28, 8, 27, 12, 823, DateTimeKind.Utc).AddTicks(1953), null, null, "Peppar" });

            migrationBuilder.InsertData(
                table: "Recipe",
                columns: new[] { "Id", "Category", "CreatedBy", "CreatedDate", "Description", "Difficulty", "LastModifiedDate", "LastmodifiedBy", "Name", "Occasion" },
                values: new object[] { new Guid("55921cce-0258-4ce3-9a7e-17b474c1d446"), 0, "Seed", new DateTime(2021, 12, 28, 8, 27, 12, 823, DateTimeKind.Utc).AddTicks(4021), "n/a", 1, null, null, "Kyckling ris & Curry", 0 });
        }
    }
}
