using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PudgeManga_Project.Data.migrations
{
    /// <inheritdoc />
    public partial class upd_Manga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Publish",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Translator",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Publish",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Translator",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Mangas");
        }
    }
}
