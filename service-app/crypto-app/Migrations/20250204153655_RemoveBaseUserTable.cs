using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crypto_app.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBaseUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_ApplicationUsers_ApplicationUserId",
                table: "BaseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_Countries_CountryId",
                table: "BaseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_Currencies_CurrencyId",
                table: "BaseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseUsers_Languages_LanguageId",
                table: "BaseUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseUsers",
                table: "BaseUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseUsers");

            migrationBuilder.RenameTable(
                name: "BaseUsers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_BaseUsers_LanguageId",
                table: "Users",
                newName: "IX_Users_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseUsers_CurrencyId",
                table: "Users",
                newName: "IX_Users_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseUsers_CountryId",
                table: "Users",
                newName: "IX_Users_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseUsers_ApplicationUserId",
                table: "Users",
                newName: "IX_Users_ApplicationUserId");

            migrationBuilder.AlterColumn<int>(
                name: "Theme",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ApplicationUsers_ApplicationUserId",
                table: "Users",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Currencies_CurrencyId",
                table: "Users",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Languages_LanguageId",
                table: "Users",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ApplicationUsers_ApplicationUserId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Countries_CountryId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Currencies_CurrencyId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Languages_LanguageId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "BaseUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Users_LanguageId",
                table: "BaseUsers",
                newName: "IX_BaseUsers_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CurrencyId",
                table: "BaseUsers",
                newName: "IX_BaseUsers_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CountryId",
                table: "BaseUsers",
                newName: "IX_BaseUsers_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ApplicationUserId",
                table: "BaseUsers",
                newName: "IX_BaseUsers_ApplicationUserId");

            migrationBuilder.AlterColumn<int>(
                name: "Theme",
                table: "BaseUsers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseUsers",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseUsers",
                table: "BaseUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUsers_ApplicationUsers_ApplicationUserId",
                table: "BaseUsers",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUsers_Countries_CountryId",
                table: "BaseUsers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUsers_Currencies_CurrencyId",
                table: "BaseUsers",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUsers_Languages_LanguageId",
                table: "BaseUsers",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
