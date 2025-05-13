using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserAddressToHomeCityAndAddAvatarUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ADDRESS",
                table: "USERS");

            migrationBuilder.AddColumn<string>(
                name: "AVATAR_URL",
                table: "USERS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HomeCityId",
                table: "USERS",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_HomeCityId",
                table: "USERS",
                column: "HomeCityId");

            migrationBuilder.AddForeignKey(
                name: "FK_USERS_CITIES_HomeCityId",
                table: "USERS",
                column: "HomeCityId",
                principalTable: "CITIES",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USERS_CITIES_HomeCityId",
                table: "USERS");

            migrationBuilder.DropIndex(
                name: "IX_USERS_HomeCityId",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "AVATAR_URL",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "HomeCityId",
                table: "USERS");

            migrationBuilder.AddColumn<string>(
                name: "ADDRESS",
                table: "USERS",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}
