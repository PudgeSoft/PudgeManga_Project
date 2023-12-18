using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PudgeManga_Project.Data.migrations
{
    /// <inheritdoc />
    public partial class Add_Rating_Models_Update_Key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aboutme",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RatingForAnimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingForAnimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingForAnimes_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "AnimeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingForAnimes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingForMangas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    AnimeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingForMangas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingForMangas_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "AnimeId");
                    table.ForeignKey(
                        name: "FK_RatingForMangas_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingForMangas_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "MangaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatingForAnimes_AnimeId",
                table: "RatingForAnimes",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingForAnimes_UserId",
                table: "RatingForAnimes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingForMangas_AnimeId",
                table: "RatingForMangas",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingForMangas_MangaId",
                table: "RatingForMangas",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingForMangas_UserId",
                table: "RatingForMangas",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingForAnimes");

            migrationBuilder.DropTable(
                name: "RatingForMangas");

            migrationBuilder.DropColumn(
                name: "Aboutme",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");
        }
    }
}
