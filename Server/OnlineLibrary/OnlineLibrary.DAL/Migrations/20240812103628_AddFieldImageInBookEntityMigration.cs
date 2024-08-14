using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineLibrary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldImageInBookEntityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Books",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("abbe8643-6536-4550-ae9b-2bc07f94bd36"), false, "User" },
                    { new Guid("f8f16246-ce18-4f8f-9657-ab5c95dd0fc4"), false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsDeleted", "Login", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { new Guid("6dc74677-d3b2-4db9-b6c7-ff09b8720cd7"), false, "Admin", "$2b$10$lq5/aYUWyNHY.LmkR8ns2.GIaVE8zvSWZO5ppLe7oq5ePL9B0zZyC", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "Id", "IsDeleted", "RoleId", "UserId" },
                values: new object[] { new Guid("9dd95e46-c605-4083-97e1-c9a538f84282"), false, new Guid("f8f16246-ce18-4f8f-9657-ab5c95dd0fc4"), new Guid("6dc74677-d3b2-4db9-b6c7-ff09b8720cd7") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("abbe8643-6536-4550-ae9b-2bc07f94bd36"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f8f16246-ce18-4f8f-9657-ab5c95dd0fc4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6dc74677-d3b2-4db9-b6c7-ff09b8720cd7"));

            migrationBuilder.DeleteData(
                table: "UsersRoles",
                keyColumn: "Id",
                keyValue: new Guid("9dd95e46-c605-4083-97e1-c9a538f84282"));

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Books");
        }
    }
}
