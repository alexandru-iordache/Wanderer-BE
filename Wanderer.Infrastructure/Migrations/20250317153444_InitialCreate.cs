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
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
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
                    NORTH_EAST_BOUND = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SOUTH_WEST_BOUND = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "TRIPS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    TITLE = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    START_DATE = table.Column<DateOnly>(type: "date", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRIPS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TRIPS_USERS_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "USERS",
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

            migrationBuilder.CreateTable(
                name: "CITY_VISITS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    START_DATE = table.Column<DateOnly>(type: "date", nullable: false),
                    NO_OF_NIGHTS = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ORDER = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CITY_VISITS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CITY_VISITS_CITIES_CityId",
                        column: x => x.CityId,
                        principalTable: "CITIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CITY_VISITS_TRIPS_TripId",
                        column: x => x.TripId,
                        principalTable: "TRIPS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DAY_VISITS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DATE = table.Column<DateOnly>(type: "date", nullable: false),
                    CityVisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DAY_VISITS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DAY_VISITS_CITY_VISITS_CityVisitId",
                        column: x => x.CityVisitId,
                        principalTable: "CITY_VISITS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WAYPOINT_VISITS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    START_TIME = table.Column<TimeOnly>(type: "time", nullable: false),
                    END_TIME = table.Column<TimeOnly>(type: "time", nullable: false),
                    WaypointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayVisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WAYPOINT_VISITS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WAYPOINT_VISITS_DAY_VISITS_DayVisitId",
                        column: x => x.DayVisitId,
                        principalTable: "DAY_VISITS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_WAYPOINT_VISITS_WAYPOINTS_WaypointId",
                        column: x => x.WaypointId,
                        principalTable: "WAYPOINTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CITIES_CountryId",
                table: "CITIES",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CITIES_PLACE_ID",
                table: "CITIES",
                column: "PLACE_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CITY_VISITS_CityId",
                table: "CITY_VISITS",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CITY_VISITS_TripId",
                table: "CITY_VISITS",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_COUNTRIES_NAME",
                table: "COUNTRIES",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DAY_VISITS_CityVisitId",
                table: "DAY_VISITS",
                column: "CityVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_TRIPS_OwnerId",
                table: "TRIPS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_EMAIL",
                table: "USERS",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WAYPOINT_VISITS_DayVisitId",
                table: "WAYPOINT_VISITS",
                column: "DayVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_WAYPOINT_VISITS_WaypointId",
                table: "WAYPOINT_VISITS",
                column: "WaypointId");

            migrationBuilder.CreateIndex(
                name: "IX_WAYPOINTS_CityId",
                table: "WAYPOINTS",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_WAYPOINTS_PLACE_ID",
                table: "WAYPOINTS",
                column: "PLACE_ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WAYPOINT_VISITS");

            migrationBuilder.DropTable(
                name: "DAY_VISITS");

            migrationBuilder.DropTable(
                name: "WAYPOINTS");

            migrationBuilder.DropTable(
                name: "CITY_VISITS");

            migrationBuilder.DropTable(
                name: "CITIES");

            migrationBuilder.DropTable(
                name: "TRIPS");

            migrationBuilder.DropTable(
                name: "COUNTRIES");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
