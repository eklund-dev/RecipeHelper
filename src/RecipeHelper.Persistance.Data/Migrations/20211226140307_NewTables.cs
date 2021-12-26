using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHelper.Persistance.Data.Migrations
{
    public partial class NewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("e8d46365-470b-4d44-a126-b4afad4cdf1c"));

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Occasion",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastmodifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IdentityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastmodifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientRecipe",
                columns: table => new
                {
                    IngredientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientRecipe", x => new { x.IngredientsId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeRecipeUser",
                columns: table => new
                {
                    FavoriteRecipesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeRecipeUser", x => new { x.FavoriteRecipesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RecipeRecipeUser_Recipes_FavoriteRecipesId",
                        column: x => x.FavoriteRecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeRecipeUser_RecipeUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "RecipeUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[] { new Guid("32187963-89d6-482f-a31e-2ca0b7868deb"), "Seed", new DateTime(2021, 12, 26, 14, 3, 6, 923, DateTimeKind.Utc).AddTicks(230), null, null, "Salt" });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[] { new Guid("f22740e1-9da7-40ce-b13e-957f5ee36c2a"), "Seed", new DateTime(2021, 12, 26, 14, 3, 6, 923, DateTimeKind.Utc).AddTicks(235), null, null, "Peppar" });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Category", "CreatedBy", "CreatedDate", "Description", "Difficulty", "LastModifiedDate", "LastmodifiedBy", "Name", "Occasion" },
                values: new object[] { new Guid("850766c4-24f9-4a1e-851f-50f32f980095"), 0, "Seed", new DateTime(2021, 12, 26, 14, 3, 6, 923, DateTimeKind.Utc).AddTicks(1683), "n/a", 1, null, null, "Kyckling ris & Curry", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRecipe_RecipesId",
                table: "IngredientRecipe",
                column: "RecipesId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeRecipeUser_UsersId",
                table: "RecipeRecipeUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientRecipe");

            migrationBuilder.DropTable(
                name: "RecipeRecipeUser");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "RecipeUsers");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: new Guid("850766c4-24f9-4a1e-851f-50f32f980095"));

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Occasion",
                table: "Recipes");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "Difficulty", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[] { new Guid("e8d46365-470b-4d44-a126-b4afad4cdf1c"), "Seed", new DateTime(2021, 12, 26, 11, 10, 16, 882, DateTimeKind.Utc).AddTicks(6838), "n/a", 1, null, null, "Kyckling ris & Curry" });
        }
    }
}
