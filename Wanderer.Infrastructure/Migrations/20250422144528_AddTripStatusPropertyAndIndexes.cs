using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTripStatusPropertyAndIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "STATUS",
                table: "TRIPS",
                type: "int",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_TRIPS_STATUS",
                table: "TRIPS",
                column: "STATUS");

            migrationBuilder.Sql("UPDATE TRIPS SET STATUS = 2 WHERE STATUS IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TRIPS_STATUS",
                table: "TRIPS");

            migrationBuilder.DropColumn(
                name: "STATUS",
                table: "TRIPS");
        }
    }
}
