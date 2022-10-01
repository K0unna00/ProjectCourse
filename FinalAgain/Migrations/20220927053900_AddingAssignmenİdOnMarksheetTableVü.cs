using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalAgain.Migrations
{
    public partial class AddingAssignmenİdOnMarksheetTableVü : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignmentId",
                table: "Marksheets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Marksheets_AssignmentId",
                table: "Marksheets",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Marksheets_Assignments_AssignmentId",
                table: "Marksheets",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marksheets_Assignments_AssignmentId",
                table: "Marksheets");

            migrationBuilder.DropIndex(
                name: "IX_Marksheets_AssignmentId",
                table: "Marksheets");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Marksheets");
        }
    }
}
