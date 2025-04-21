using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T1PR2_APIREST.Migrations
{
    /// <inheritdoc />
    public partial class Final1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_UserId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Games_GameId",
                table: "Votes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Votes",
                table: "Votes");

            migrationBuilder.RenameTable(
                name: "Votes",
                newName: "FavGames");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_UserId_GameId",
                table: "FavGames",
                newName: "IX_FavGames_UserId_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_GameId",
                table: "FavGames",
                newName: "IX_FavGames_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavGames",
                table: "FavGames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavGames_AspNetUsers_UserId",
                table: "FavGames",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavGames_Games_GameId",
                table: "FavGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavGames_AspNetUsers_UserId",
                table: "FavGames");

            migrationBuilder.DropForeignKey(
                name: "FK_FavGames_Games_GameId",
                table: "FavGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavGames",
                table: "FavGames");

            migrationBuilder.RenameTable(
                name: "FavGames",
                newName: "Votes");

            migrationBuilder.RenameIndex(
                name: "IX_FavGames_UserId_GameId",
                table: "Votes",
                newName: "IX_Votes_UserId_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_FavGames_GameId",
                table: "Votes",
                newName: "IX_Votes_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Votes",
                table: "Votes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_AspNetUsers_UserId",
                table: "Votes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Games_GameId",
                table: "Votes",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
