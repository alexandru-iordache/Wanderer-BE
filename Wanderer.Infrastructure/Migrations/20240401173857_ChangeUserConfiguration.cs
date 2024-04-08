using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PASSWORD_HASH",
                table: "USERS");

            migrationBuilder.AlterColumn<string>(
                name: "LAST_NAME",
                table: "USERS",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "FIRST_NAME",
                table: "USERS",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<DateTime>(
                name: "BIRTH_DATE",
                table: "USERS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PROFILE_NAME",
                table: "USERS",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_PROFILE_NAME",
                table: "USERS",
                column: "PROFILE_NAME",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_USERS_PROFILE_NAME",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "BIRTH_DATE",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "PROFILE_NAME",
                table: "USERS");

            migrationBuilder.AlterColumn<string>(
                name: "LAST_NAME",
                table: "USERS",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FIRST_NAME",
                table: "USERS",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PASSWORD_HASH",
                table: "USERS",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}
