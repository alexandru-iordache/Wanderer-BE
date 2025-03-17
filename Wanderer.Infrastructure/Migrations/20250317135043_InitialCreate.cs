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
                    NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    FIRST_NAME = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ADDRESS = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CITIES",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PLACE_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NorthEastBoundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SouthWestBoundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CITIES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CITIES_COUNTRIES_CountryId",
                        column: x => x.CountryId,
                        principalTable: "COUNTRIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LAT_LNG_BOUNDS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LATITUDE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LONGITUDE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LAT_LNG_BOUNDS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LAT_LNG_BOUNDS_CITIES_CityId",
                        column: x => x.CityId,
                        principalTable: "CITIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WAYPOINTS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PLACE_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TYPE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WAYPOINTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WAYPOINTS_CITIES_CityId",
                        column: x => x.CityId,
                        principalTable: "CITIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CITIES_CountryId",
                table: "CITIES",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CITIES_NorthEastBoundId",
                table: "CITIES",
                column: "NorthEastBoundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CITIES_PLACE_ID",
                table: "CITIES",
                column: "PLACE_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CITIES_SouthWestBoundId",
                table: "CITIES",
                column: "SouthWestBoundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COUNTRIES_NAME",
                table: "COUNTRIES",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LAT_LNG_BOUNDS_CityId",
                table: "LAT_LNG_BOUNDS",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_EMAIL",
                table: "USERS",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WAYPOINTS_CityId",
                table: "WAYPOINTS",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_WAYPOINTS_PLACE_ID",
                table: "WAYPOINTS",
                column: "PLACE_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CITIES_LAT_LNG_BOUNDS_NorthEastBoundId",
                table: "CITIES",
                column: "NorthEastBoundId",
                principalTable: "LAT_LNG_BOUNDS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CITIES_LAT_LNG_BOUNDS_SouthWestBoundId",
                table: "CITIES",
                column: "SouthWestBoundId",
                principalTable: "LAT_LNG_BOUNDS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CITIES_COUNTRIES_CountryId",
                table: "CITIES");

            migrationBuilder.DropForeignKey(
                name: "FK_CITIES_LAT_LNG_BOUNDS_NorthEastBoundId",
                table: "CITIES");

            migrationBuilder.DropForeignKey(
                name: "FK_CITIES_LAT_LNG_BOUNDS_SouthWestBoundId",
                table: "CITIES");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "WAYPOINTS");

            migrationBuilder.DropTable(
                name: "COUNTRIES");

            migrationBuilder.DropTable(
                name: "LAT_LNG_BOUNDS");

            migrationBuilder.DropTable(
                name: "CITIES");
        }
    }
}
