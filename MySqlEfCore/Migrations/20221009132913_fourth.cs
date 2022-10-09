using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MySqlEfCore.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "QuizGames");

            migrationBuilder.AddColumn<double>(
                name: "PlayerLatitude",
                table: "QuizGames",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PlayerLongitude",
                table: "QuizGames",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PlayerName",
                table: "QuizGames",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LocationHintUsed",
                table: "QuizGameQuestions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "QuestionHintUsed",
                table: "QuizGameQuestions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Controls",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    GPSRadius = table.Column<int>(nullable: false),
                    THOver = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controls", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Controls");

            migrationBuilder.DropColumn(
                name: "PlayerLatitude",
                table: "QuizGames");

            migrationBuilder.DropColumn(
                name: "PlayerLongitude",
                table: "QuizGames");

            migrationBuilder.DropColumn(
                name: "PlayerName",
                table: "QuizGames");

            migrationBuilder.DropColumn(
                name: "LocationHintUsed",
                table: "QuizGameQuestions");

            migrationBuilder.DropColumn(
                name: "QuestionHintUsed",
                table: "QuizGameQuestions");

            migrationBuilder.AddColumn<byte[]>(
                name: "PlayerId",
                table: "QuizGames",
                type: "varbinary(16)",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    PlayerName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });
        }
    }
}
