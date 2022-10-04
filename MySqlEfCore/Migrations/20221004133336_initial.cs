using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace MySqlEfCore.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaderboardEntries",
                columns: table => new
                {
                    LeaderboardId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TeamCode = table.Column<string>(nullable: true),
                    TeamName = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    League = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderboardEntries", x => x.LeaderboardId);
                });

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Age = table.Column<int>(nullable: false),
                    IsPlayer = table.Column<ulong>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PlayerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<byte[]>(nullable: false),
                    QuestionType = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    QuestionText = table.Column<string>(nullable: true),
                    AnswerA = table.Column<string>(nullable: true),
                    AnswerB = table.Column<string>(nullable: true),
                    AnswerC = table.Column<string>(nullable: true),
                    AnswerD = table.Column<string>(nullable: true),
                    CorrectAnswer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "QuizCategoryLengths",
                columns: table => new
                {
                    QuizCategoryLengthId = table.Column<byte[]>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    NumberOfQuestions = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizCategoryLengths", x => x.QuizCategoryLengthId);
                });

            migrationBuilder.CreateTable(
                name: "QuizGameQuestions",
                columns: table => new
                {
                    QuizGameQuestionId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    QuizGameId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    QuestionPosition = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizGameQuestions", x => x.QuizGameQuestionId);
                });

            migrationBuilder.CreateTable(
                name: "QuizGames",
                columns: table => new
                {
                    QuizGameId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AppId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    QuizGameLength = table.Column<int>(nullable: false),
                    CurrentQuestionPosition = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizGames", x => x.QuizGameId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "LeaderboardEntries");

            migrationBuilder.DropTable(
                name: "person");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "QuizCategoryLengths");

            migrationBuilder.DropTable(
                name: "QuizGameQuestions");

            migrationBuilder.DropTable(
                name: "QuizGames");
        }
    }
}
