using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PudgeManga_Project.Data.migrations
{
    /// <inheritdoc />
    public partial class current : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Popularities",
                table: "Popularities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Popularities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Popularities",
                table: "Popularities",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ChapterId",
                table: "Pages",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MangaId",
                table: "Comments",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_MangaID",
                table: "Chapters",
                column: "MangaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Mangas_MangaID",
                table: "Chapters",
                column: "MangaID",
                principalTable: "Mangas",
                principalColumn: "MangaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Mangas_MangaId",
                table: "Comments",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "MangaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Chapters_ChapterId",
                table: "Pages",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "ChapterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Popularities_Mangas_MangaId",
                table: "Popularities",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "MangaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Mangas_MangaID",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Mangas_MangaId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Chapters_ChapterId",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Popularities_Mangas_MangaId",
                table: "Popularities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Popularities",
                table: "Popularities");

            migrationBuilder.DropIndex(
                name: "IX_Pages_ChapterId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MangaId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Chapters_MangaID",
                table: "Chapters");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Popularities",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Popularities",
                table: "Popularities",
                column: "Id");
        }
    }
}
