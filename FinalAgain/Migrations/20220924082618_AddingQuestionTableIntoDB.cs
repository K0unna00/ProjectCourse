using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalAgain.Migrations
{
    public partial class AddingQuestionTableIntoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Question_QuestionId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_AspNetUsers_AdminId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Marksheets_MarksheetId",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameIndex(
                name: "IX_Question_MarksheetId",
                table: "Questions",
                newName: "IX_Questions_MarksheetId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_AdminId",
                table: "Questions",
                newName: "IX_Questions_AdminId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Questions_QuestionId",
                table: "Marks",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AspNetUsers_AdminId",
                table: "Questions",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Marksheets_MarksheetId",
                table: "Questions",
                column: "MarksheetId",
                principalTable: "Marksheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Questions_QuestionId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AspNetUsers_AdminId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Marksheets_MarksheetId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_MarksheetId",
                table: "Question",
                newName: "IX_Question_MarksheetId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_AdminId",
                table: "Question",
                newName: "IX_Question_AdminId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Question_QuestionId",
                table: "Marks",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_AspNetUsers_AdminId",
                table: "Question",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Marksheets_MarksheetId",
                table: "Question",
                column: "MarksheetId",
                principalTable: "Marksheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
