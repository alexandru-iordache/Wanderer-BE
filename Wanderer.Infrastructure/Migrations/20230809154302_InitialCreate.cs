using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COUNTRIES",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUNTRIES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ADDRESS = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PASSWORD_HASH = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CITIES",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    COUNTRY_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CITIES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CITIES_COUNTRIES_COUNTRY_ID",
                        column: x => x.COUNTRY_ID,
                        principalTable: "COUNTRIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CITIES_COUNTRY_ID",
                table: "CITIES",
                column: "COUNTRY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COUNTRIES_NAME",
                table: "COUNTRIES",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_EMAIL",
                table: "USERS",
                column: "EMAIL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CITIES");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "COUNTRIES");
        }
    }
}
