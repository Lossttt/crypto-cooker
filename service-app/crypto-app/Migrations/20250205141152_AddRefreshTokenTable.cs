using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crypto_app.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "ApplicationUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LoginToken",
                table: "ApplicationUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "ApplicationUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "ApplicationUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "AspNetRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    Expires = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRefreshTokens_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRefreshTokens_UserId",
                table: "AspNetRefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRefreshTokens");

            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "LoginToken",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "ApplicationUsers");
        }
    }
}
