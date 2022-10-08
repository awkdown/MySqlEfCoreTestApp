using Microsoft.EntityFrameworkCore.Migrations;

namespace MySqlEfCore.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hint",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "QuestionLatitude",
                table: "Questions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "QuestionLongitude",
                table: "Questions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "UseLatLong",
                table: "Questions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Categories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hint",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionLatitude",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionLongitude",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "UseLatLong",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Categories");
        }
    }
}
