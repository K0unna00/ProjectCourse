using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalAgain.Migrations
{
    public partial class AddingTotalGradeOnQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgGrade",
                table: "Marksheets");

            migrationBuilder.AddColumn<int>(
                name: "GradeLimit",
                table: "Questions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalGrade",
                table: "Questions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradeLimit",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TotalGrade",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "AvgGrade",
                table: "Marksheets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
