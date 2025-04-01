using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorUSersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FIRST_NAME",
                table: "USERS");

            migrationBuilder.RenameColumn(
                name: "LAST_NAME",
                table: "USERS",
                newName: "PROFILE_NAME");

            migrationBuilder.AlterColumn<string>(
                name: "ADDRESS",
                table: "USERS",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PROFILE_NAME",
                table: "USERS",
                newName: "LAST_NAME");

            migrationBuilder.AlterColumn<string>(
                name: "ADDRESS",
                table: "USERS",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FIRST_NAME",
                table: "USERS",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
