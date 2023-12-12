using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PudgeManga_Project.Data.migrations
{
    /// <inheritdoc />
    public partial class addmangaCommentstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MangaComment_Comments_CommentId",
                table: "MangaComment");

            migrationBuilder.DropForeignKey(
                name: "FK_MangaComment_Mangas_MangaId",
                table: "MangaComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MangaComment",
                table: "MangaComment");

            migrationBuilder.RenameTable(
                name: "MangaComment",
                newName: "CommentsForManga");

            migrationBuilder.RenameIndex(
                name: "IX_MangaComment_CommentId",
                table: "CommentsForManga",
                newName: "IX_CommentsForManga_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentsForManga",
                table: "CommentsForManga",
                columns: new[] { "MangaId", "CommentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsForManga_Comments_CommentId",
                table: "CommentsForManga",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsForManga_Mangas_MangaId",
                table: "CommentsForManga",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "MangaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentsForManga_Comments_CommentId",
                table: "CommentsForManga");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentsForManga_Mangas_MangaId",
                table: "CommentsForManga");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentsForManga",
                table: "CommentsForManga");

            migrationBuilder.RenameTable(
                name: "CommentsForManga",
                newName: "MangaComment");

            migrationBuilder.RenameIndex(
                name: "IX_CommentsForManga_CommentId",
                table: "MangaComment",
                newName: "IX_MangaComment_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MangaComment",
                table: "MangaComment",
                columns: new[] { "MangaId", "CommentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MangaComment_Comments_CommentId",
                table: "MangaComment",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MangaComment_Mangas_MangaId",
                table: "MangaComment",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "MangaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
