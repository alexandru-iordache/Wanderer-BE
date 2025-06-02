using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPrincipalKeyForPostComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_POST_COMMENTS",
                table: "POST_COMMENTS");

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "POST_COMMENTS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_POST_COMMENTS",
                table: "POST_COMMENTS",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_POST_COMMENTS_PostId",
                table: "POST_COMMENTS",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_POST_COMMENTS",
                table: "POST_COMMENTS");

            migrationBuilder.DropIndex(
                name: "IX_POST_COMMENTS_PostId",
                table: "POST_COMMENTS");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "POST_COMMENTS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_POST_COMMENTS",
                table: "POST_COMMENTS",
                columns: new[] { "PostId", "UserId", "CREATED_AT" });
        }
    }
}
