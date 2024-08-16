using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineLibrary.DAL.Migrations
{
    public partial class ChangedFieldTypeInUserRoleEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"ALTER TABLE ""UsersRoles"" 
                  ALTER COLUMN ""IsDeleted"" TYPE boolean 
                  USING CASE 
                        WHEN ""IsDeleted""::text IN ('true', '1') THEN TRUE 
                        ELSE FALSE 
                       END;"
            );
            
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "UsersRoles",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"ALTER TABLE ""UsersRoles"" 
                  ALTER COLUMN ""IsDeleted"" TYPE uuid 
                  USING CASE 
                        WHEN ""IsDeleted"" THEN '1'::uuid 
                        ELSE '0'::uuid 
                       END;"
            );
            
            migrationBuilder.AlterColumn<Guid>(
                name: "IsDeleted",
                table: "UsersRoles",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }
    }
}