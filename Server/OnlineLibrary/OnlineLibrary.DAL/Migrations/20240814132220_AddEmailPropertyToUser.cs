using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLibrary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailPropertyToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("abbe8643-6536-4550-ae9b-2bc07f94bd36"));

            migrationBuilder.DeleteData(
                table: "UsersRoles",
                keyColumn: "Id",
                keyValue: new Guid("9dd95e46-c605-4083-97e1-c9a538f84282"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { new Guid("17942e23-50d4-4771-a280-6691932bc6b6"), false, "User" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6dc74677-d3b2-4db9-b6c7-ff09b8720cd7"),
                columns: new[] { "Email", "PasswordHash" },
                values: new object[] { "admin@gmail.com", "$2b$10$qIfpOf.rULIWEpngOMXFBO7Lo03XUEhDZ3H.6bsMM1FVTvZRM386u" });

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "Id", "IsDeleted", "RoleId", "UserId" },
                values: new object[] { new Guid("0e1d83af-268a-46bf-bcb4-c459adeba085"), false, new Guid("f8f16246-ce18-4f8f-9657-ab5c95dd0fc4"), new Guid("6dc74677-d3b2-4db9-b6c7-ff09b8720cd7") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("17942e23-50d4-4771-a280-6691932bc6b6"));

            migrationBuilder.DeleteData(
                table: "UsersRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e1d83af-268a-46bf-bcb4-c459adeba085"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { new Guid("abbe8643-6536-4550-ae9b-2bc07f94bd36"), false, "User" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6dc74677-d3b2-4db9-b6c7-ff09b8720cd7"),
                column: "PasswordHash",
                value: "$2b$10$lq5/aYUWyNHY.LmkR8ns2.GIaVE8zvSWZO5ppLe7oq5ePL9B0zZyC");

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "Id", "IsDeleted", "RoleId", "UserId" },
                values: new object[] { new Guid("9dd95e46-c605-4083-97e1-c9a538f84282"), false, new Guid("f8f16246-ce18-4f8f-9657-ab5c95dd0fc4"), new Guid("6dc74677-d3b2-4db9-b6c7-ff09b8720cd7") });
        }
    }
}
