using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveDescriptionFieldAndSetPrecisionForLatAndLongProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DESCRIPTION",
                table: "WAYPOINTS");

            migrationBuilder.DropColumn(
                name: "DESCRIPTION",
                table: "COUNTRIES");

            migrationBuilder.DropColumn(
                name: "DESCRIPTION",
                table: "CITIES");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "WAYPOINTS",
                newName: "LONGITUDE");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "WAYPOINTS",
                newName: "LATITUDE");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "CITIES",
                newName: "LONGITUDE");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "CITIES",
                newName: "LATITUDE");

            migrationBuilder.AlterColumn<decimal>(
                name: "LONGITUDE",
                table: "WAYPOINTS",
                type: "decimal(9,6)",
                precision: 9,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LATITUDE",
                table: "WAYPOINTS",
                type: "decimal(9,6)",
                precision: 9,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "DESCRIPTION",
                table: "WAYPOINT_VISITS",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DESCRIPTION",
                table: "DAY_VISITS",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DESCRIPTION",
                table: "CITY_VISITS",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LONGITUDE",
                table: "CITIES",
                type: "decimal(9,6)",
                precision: 9,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LATITUDE",
                table: "CITIES",
                type: "decimal(9,6)",
                precision: 9,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DESCRIPTION",
                table: "WAYPOINT_VISITS");

            migrationBuilder.DropColumn(
                name: "DESCRIPTION",
                table: "DAY_VISITS");

            migrationBuilder.DropColumn(
                name: "DESCRIPTION",
                table: "CITY_VISITS");

            migrationBuilder.RenameColumn(
                name: "LONGITUDE",
                table: "WAYPOINTS",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "LATITUDE",
                table: "WAYPOINTS",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "LONGITUDE",
                table: "CITIES",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "LATITUDE",
                table: "CITIES",
                newName: "Latitude");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "WAYPOINTS",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)",
                oldPrecision: 9,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "WAYPOINTS",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)",
                oldPrecision: 9,
                oldScale: 6);

            migrationBuilder.AddColumn<string>(
                name: "DESCRIPTION",
                table: "WAYPOINTS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DESCRIPTION",
                table: "COUNTRIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "CITIES",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)",
                oldPrecision: 9,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "CITIES",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)",
                oldPrecision: 9,
                oldScale: 6);

            migrationBuilder.AddColumn<string>(
                name: "DESCRIPTION",
                table: "CITIES",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
