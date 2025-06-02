using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelantionshipBetweenPostImageAndCitiesOrWaypoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityPlaceId",
                table: "POST_IMAGES",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaypointPlaceId",
                table: "POST_IMAGES",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_WAYPOINTS_PLACE_ID",
                table: "WAYPOINTS",
                column: "PLACE_ID");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CITIES_PLACE_ID",
                table: "CITIES",
                column: "PLACE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_POST_IMAGES_CityPlaceId",
                table: "POST_IMAGES",
                column: "CityPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_POST_IMAGES_WaypointPlaceId",
                table: "POST_IMAGES",
                column: "WaypointPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_POST_IMAGES_CITIES_CityPlaceId",
                table: "POST_IMAGES",
                column: "CityPlaceId",
                principalTable: "CITIES",
                principalColumn: "PLACE_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_POST_IMAGES_WAYPOINTS_WaypointPlaceId",
                table: "POST_IMAGES",
                column: "WaypointPlaceId",
                principalTable: "WAYPOINTS",
                principalColumn: "PLACE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POST_IMAGES_CITIES_CityPlaceId",
                table: "POST_IMAGES");

            migrationBuilder.DropForeignKey(
                name: "FK_POST_IMAGES_WAYPOINTS_WaypointPlaceId",
                table: "POST_IMAGES");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_WAYPOINTS_PLACE_ID",
                table: "WAYPOINTS");

            migrationBuilder.DropIndex(
                name: "IX_POST_IMAGES_CityPlaceId",
                table: "POST_IMAGES");

            migrationBuilder.DropIndex(
                name: "IX_POST_IMAGES_WaypointPlaceId",
                table: "POST_IMAGES");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CITIES_PLACE_ID",
                table: "CITIES");

            migrationBuilder.DropColumn(
                name: "CityPlaceId",
                table: "POST_IMAGES");

            migrationBuilder.DropColumn(
                name: "WaypointPlaceId",
                table: "POST_IMAGES");
        }
    }
}
