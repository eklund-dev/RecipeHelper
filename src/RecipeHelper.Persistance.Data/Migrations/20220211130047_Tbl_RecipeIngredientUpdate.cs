using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHelper.Persistance.Data.Migrations
{
    public partial class Tbl_RecipeIngredientUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredient_RecipeId",
                table: "RecipeIngredient");

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("009730f0-f81b-4ead-bd32-23e4f5746781"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("1b755e2e-462c-4ed7-9319-6d11346285fe"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("24f6f5e6-9a03-4510-bfbc-93cdcb135faf"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("64fe636f-d58b-4c94-bd4c-082cfad86c88"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("cf355479-2469-402d-b4dd-1de243f49244"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("e019b04c-347a-4883-9cfb-e9b7fa960d88"));

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: new Guid("0e529c64-d5ae-4b86-86f5-4ac6981bbcc0"));

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: new Guid("126f8e03-15db-46ae-95a6-bacbd4f92760"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipeIngredient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient",
                columns: new[] { "RecipeId", "IngredientId" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[] { new Guid("c61cccf1-856e-4bc9-ad09-46bfaf481fcf"), "Seed", new DateTime(2022, 2, 11, 13, 0, 47, 272, DateTimeKind.Utc).AddTicks(113), null, null, "Simple and tasty" });

            migrationBuilder.InsertData(
                table: "FoodType",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("102e0408-dcb2-4fbf-9062-dc89c7442fd3"), "Seed", new DateTime(2022, 2, 11, 13, 0, 47, 272, DateTimeKind.Utc).AddTicks(4427), null, null, "Meat" },
                    { new Guid("52c7fa23-7745-4111-a5cd-605d687fdb9d"), "Seed", new DateTime(2022, 2, 11, 13, 0, 47, 272, DateTimeKind.Utc).AddTicks(4431), null, null, "Fish" },
                    { new Guid("95a5029d-b617-4a91-bf25-65c8a69033ca"), "Seed", new DateTime(2022, 2, 11, 13, 0, 47, 272, DateTimeKind.Utc).AddTicks(4434), null, null, "Vegatarian" },
                    { new Guid("a246a5a0-1f68-44ec-8734-f89b33906d2e"), "Seed", new DateTime(2022, 2, 11, 13, 0, 47, 272, DateTimeKind.Utc).AddTicks(4432), null, null, "Chicken" },
                    { new Guid("e167d240-e2d8-4916-8f62-765987d92702"), "Seed", new DateTime(2022, 2, 11, 13, 0, 47, 272, DateTimeKind.Utc).AddTicks(4435), null, null, "Vegan" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("5cef7167-95bd-4891-9dfa-1e408fe58a2d"), "Seed", new DateTime(2022, 2, 11, 13, 0, 47, 272, DateTimeKind.Utc).AddTicks(6016), null, null, "Salt" },
                    { new Guid("aae1b145-cb2c-4660-a28b-6af41a3c5d23"), "Seed", new DateTime(2022, 2, 11, 13, 0, 47, 272, DateTimeKind.Utc).AddTicks(6018), null, null, "Peppar" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient");

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("c61cccf1-856e-4bc9-ad09-46bfaf481fcf"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("102e0408-dcb2-4fbf-9062-dc89c7442fd3"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("52c7fa23-7745-4111-a5cd-605d687fdb9d"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("95a5029d-b617-4a91-bf25-65c8a69033ca"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("a246a5a0-1f68-44ec-8734-f89b33906d2e"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("e167d240-e2d8-4916-8f62-765987d92702"));

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: new Guid("5cef7167-95bd-4891-9dfa-1e408fe58a2d"));

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: new Guid("aae1b145-cb2c-4660-a28b-6af41a3c5d23"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RecipeIngredient",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[] { new Guid("009730f0-f81b-4ead-bd32-23e4f5746781"), "Seed", new DateTime(2022, 2, 10, 16, 36, 21, 83, DateTimeKind.Utc).AddTicks(4025), null, null, "Simple and tasty" });

            migrationBuilder.InsertData(
                table: "FoodType",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("1b755e2e-462c-4ed7-9319-6d11346285fe"), "Seed", new DateTime(2022, 2, 10, 16, 36, 21, 83, DateTimeKind.Utc).AddTicks(8063), null, null, "Meat" },
                    { new Guid("24f6f5e6-9a03-4510-bfbc-93cdcb135faf"), "Seed", new DateTime(2022, 2, 10, 16, 36, 21, 83, DateTimeKind.Utc).AddTicks(8067), null, null, "Fish" },
                    { new Guid("64fe636f-d58b-4c94-bd4c-082cfad86c88"), "Seed", new DateTime(2022, 2, 10, 16, 36, 21, 83, DateTimeKind.Utc).AddTicks(8070), null, null, "Vegatarian" },
                    { new Guid("cf355479-2469-402d-b4dd-1de243f49244"), "Seed", new DateTime(2022, 2, 10, 16, 36, 21, 83, DateTimeKind.Utc).AddTicks(8071), null, null, "Vegan" },
                    { new Guid("e019b04c-347a-4883-9cfb-e9b7fa960d88"), "Seed", new DateTime(2022, 2, 10, 16, 36, 21, 83, DateTimeKind.Utc).AddTicks(8068), null, null, "Chicken" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("0e529c64-d5ae-4b86-86f5-4ac6981bbcc0"), "Seed", new DateTime(2022, 2, 10, 16, 36, 21, 84, DateTimeKind.Utc).AddTicks(1), null, null, "Peppar" },
                    { new Guid("126f8e03-15db-46ae-95a6-bacbd4f92760"), "Seed", new DateTime(2022, 2, 10, 16, 36, 21, 83, DateTimeKind.Utc).AddTicks(9999), null, null, "Salt" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_RecipeId",
                table: "RecipeIngredient",
                column: "RecipeId");
        }
    }
}
