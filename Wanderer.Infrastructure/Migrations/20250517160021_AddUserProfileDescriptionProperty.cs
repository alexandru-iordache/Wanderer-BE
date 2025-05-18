using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfileDescriptionProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PROFILE_DESCRIPTION",
                table: "USERS",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PROFILE_DESCRIPTION",
                table: "USERS");
        }
    }
}
