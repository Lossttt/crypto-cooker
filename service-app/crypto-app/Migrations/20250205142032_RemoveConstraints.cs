using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crypto_app.Migrations
{
    /// <inheritdoc />
    public partial class RemoveConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_ApplicationAccessCode",
                table: "ApplicationUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_ApplicationAccessCode",
                table: "ApplicationUsers",
                column: "ApplicationAccessCode",
                unique: true);
        }
    }
}
