using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteBehaviorForDayVisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WAYPOINT_VISITS_DAY_VISITS_DayVisitId",
                table: "WAYPOINT_VISITS");

            migrationBuilder.DropForeignKey(
                name: "FK_WAYPOINT_VISITS_WAYPOINTS_WaypointId",
                table: "WAYPOINT_VISITS");

            migrationBuilder.AddForeignKey(
                name: "FK_WAYPOINT_VISITS_DAY_VISITS_DayVisitId",
                table: "WAYPOINT_VISITS",
                column: "DayVisitId",
                principalTable: "DAY_VISITS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WAYPOINT_VISITS_WAYPOINTS_WaypointId",
                table: "WAYPOINT_VISITS",
                column: "WaypointId",
                principalTable: "WAYPOINTS",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WAYPOINT_VISITS_DAY_VISITS_DayVisitId",
                table: "WAYPOINT_VISITS");

            migrationBuilder.DropForeignKey(
                name: "FK_WAYPOINT_VISITS_WAYPOINTS_WaypointId",
                table: "WAYPOINT_VISITS");

            migrationBuilder.AddForeignKey(
                name: "FK_WAYPOINT_VISITS_DAY_VISITS_DayVisitId",
                table: "WAYPOINT_VISITS",
                column: "DayVisitId",
                principalTable: "DAY_VISITS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_WAYPOINT_VISITS_WAYPOINTS_WaypointId",
                table: "WAYPOINT_VISITS",
                column: "WaypointId",
                principalTable: "WAYPOINTS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
