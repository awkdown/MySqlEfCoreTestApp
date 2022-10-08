using Microsoft.EntityFrameworkCore.Migrations;

namespace MySqlEfCore.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "QuestionLongitude",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "QuestionLatitude",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "QuestionLongitude",
                table: "Questions",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "QuestionLatitude",
                table: "Questions",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
