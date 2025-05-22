using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPostTableStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POSTS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    TITLE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_POSTS_TRIPS_TripId",
                        column: x => x.TripId,
                        principalTable: "TRIPS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSTS_USERS_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "USERS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "POST_COMMENTS",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POST_COMMENTS", x => new { x.PostId, x.UserId, x.CREATED_AT });
                    table.ForeignKey(
                        name: "FK_POST_COMMENTS_POSTS_PostId",
                        column: x => x.PostId,
                        principalTable: "POSTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POST_COMMENTS_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "POST_IMAGES",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IMAGE_URL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POST_IMAGES", x => new { x.PostId, x.IMAGE_URL });
                    table.ForeignKey(
                        name: "FK_POST_IMAGES_POSTS_PostId",
                        column: x => x.PostId,
                        principalTable: "POSTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "POST_LIKES",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POST_LIKES", x => new { x.PostId, x.UserId });
                    table.ForeignKey(
                        name: "FK_POST_LIKES_POSTS_PostId",
                        column: x => x.PostId,
                        principalTable: "POSTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POST_LIKES_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_POST_COMMENTS_UserId",
                table: "POST_COMMENTS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_POST_LIKES_UserId",
                table: "POST_LIKES",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_POSTS_OwnerId",
                table: "POSTS",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_POSTS_TripId",
                table: "POSTS",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POST_COMMENTS");

            migrationBuilder.DropTable(
                name: "POST_IMAGES");

            migrationBuilder.DropTable(
                name: "POST_LIKES");

            migrationBuilder.DropTable(
                name: "POSTS");
        }
    }
}
