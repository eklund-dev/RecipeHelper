using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHelper.Persistance.Data.Migrations
{
    public partial class Tbl_FavoriteRecipe_Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("e4c56d33-d043-4730-84f5-0d609bdb2a57"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("0d5440dc-cf1e-418c-a530-d20b23a4eb3f"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("3827d999-4ba4-433d-9b34-153e34c6a8f0"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("a7d82206-0e6d-4c17-a100-fd7278809dc5"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("b94d7db2-bc89-412a-8706-9e72d32015c2"));

            migrationBuilder.DeleteData(
                table: "FoodType",
                keyColumn: "Id",
                keyValue: new Guid("d4fb7ac1-c3d6-48b0-8b5c-dc78635d3402"));

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: new Guid("01196137-b6fa-4cc4-aecb-41c007f484ab"));

            migrationBuilder.DeleteData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: new Guid("ac4f8e98-0b03-4494-ba42-41693953ba15"));

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FavoriteRecipe");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FavoriteRecipe");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "FavoriteRecipe");

            migrationBuilder.DropColumn(
                name: "LastmodifiedBy",
                table: "FavoriteRecipe");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FavoriteRecipe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "FavoriteRecipe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "FavoriteRecipe",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastmodifiedBy",
                table: "FavoriteRecipe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[] { new Guid("e4c56d33-d043-4730-84f5-0d609bdb2a57"), "Seed", new DateTime(2022, 2, 9, 15, 24, 52, 537, DateTimeKind.Utc).AddTicks(2871), null, null, "Simple and tasty" });

            migrationBuilder.InsertData(
                table: "FoodType",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("0d5440dc-cf1e-418c-a530-d20b23a4eb3f"), "Seed", new DateTime(2022, 2, 9, 15, 24, 52, 538, DateTimeKind.Utc).AddTicks(2037), null, null, "Vegan" },
                    { new Guid("3827d999-4ba4-433d-9b34-153e34c6a8f0"), "Seed", new DateTime(2022, 2, 9, 15, 24, 52, 538, DateTimeKind.Utc).AddTicks(2023), null, null, "Fish" },
                    { new Guid("a7d82206-0e6d-4c17-a100-fd7278809dc5"), "Seed", new DateTime(2022, 2, 9, 15, 24, 52, 538, DateTimeKind.Utc).AddTicks(2019), null, null, "Meat" },
                    { new Guid("b94d7db2-bc89-412a-8706-9e72d32015c2"), "Seed", new DateTime(2022, 2, 9, 15, 24, 52, 538, DateTimeKind.Utc).AddTicks(2034), null, null, "Chicken" },
                    { new Guid("d4fb7ac1-c3d6-48b0-8b5c-dc78635d3402"), "Seed", new DateTime(2022, 2, 9, 15, 24, 52, 538, DateTimeKind.Utc).AddTicks(2036), null, null, "Vegatarian" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedDate", "LastmodifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("01196137-b6fa-4cc4-aecb-41c007f484ab"), "Seed", new DateTime(2022, 2, 9, 15, 24, 52, 538, DateTimeKind.Utc).AddTicks(3300), null, null, "Salt" },
                    { new Guid("ac4f8e98-0b03-4494-ba42-41693953ba15"), "Seed", new DateTime(2022, 2, 9, 15, 24, 52, 538, DateTimeKind.Utc).AddTicks(3302), null, null, "Peppar" }
                });
        }
    }
}
