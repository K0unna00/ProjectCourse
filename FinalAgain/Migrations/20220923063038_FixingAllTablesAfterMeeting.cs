using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalAgain.Migrations
{
    public partial class FixingAllTablesAfterMeeting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Marksheet_MarksheetId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_AspNetUsers_UserId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marksheet_Assignments_AssignmentId",
                table: "Marksheet");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMarksheets_Marksheet_MarksheetId",
                table: "UserMarksheets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMarksheets_AspNetUsers_UserId",
                table: "UserMarksheets");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_UserMarksheets_UserId",
                table: "UserMarksheets");

            migrationBuilder.DropIndex(
                name: "IX_Marks_MarksheetId",
                table: "Marks");

            migrationBuilder.DropIndex(
                name: "IX_Marks_UserId",
                table: "Marks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marksheet",
                table: "Marksheet");

            migrationBuilder.DropIndex(
                name: "IX_Marksheet_AssignmentId",
                table: "Marksheet");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserMarksheets");

            migrationBuilder.DropColumn(
                name: "MarksheetId",
                table: "Marks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Marks");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Marksheet");

            migrationBuilder.RenameTable(
                name: "Marksheet",
                newName: "Marksheets");

            migrationBuilder.AddColumn<string>(
                name: "MarkerId",
                table: "UserMarksheets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarkerId",
                table: "Marks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Marks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Marksheets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Marksheets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Marksheets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamNo",
                table: "Marksheets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marksheets",
                table: "Marksheets",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarksheetId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    AdminId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Marksheets_MarksheetId",
                        column: x => x.MarksheetId,
                        principalTable: "Marksheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    MarksheetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Marksheets_MarksheetId",
                        column: x => x.MarksheetId,
                        principalTable: "Marksheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMarksheets_MarkerId",
                table: "UserMarksheets",
                column: "MarkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_MarkerId",
                table: "Marks",
                column: "MarkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_QuestionId",
                table: "Marks",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Marksheets_AdminId",
                table: "Marksheets",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_AdminId",
                table: "Question",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_MarksheetId",
                table: "Question",
                column: "MarksheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_MarksheetId",
                table: "Students",
                column: "MarksheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_AspNetUsers_MarkerId",
                table: "Marks",
                column: "MarkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Question_QuestionId",
                table: "Marks",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marksheets_AspNetUsers_AdminId",
                table: "Marksheets",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMarksheets_AspNetUsers_MarkerId",
                table: "UserMarksheets",
                column: "MarkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMarksheets_Marksheets_MarksheetId",
                table: "UserMarksheets",
                column: "MarksheetId",
                principalTable: "Marksheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_AspNetUsers_MarkerId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Question_QuestionId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marksheets_AspNetUsers_AdminId",
                table: "Marksheets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMarksheets_AspNetUsers_MarkerId",
                table: "UserMarksheets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMarksheets_Marksheets_MarksheetId",
                table: "UserMarksheets");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropIndex(
                name: "IX_UserMarksheets_MarkerId",
                table: "UserMarksheets");

            migrationBuilder.DropIndex(
                name: "IX_Marks_MarkerId",
                table: "Marks");

            migrationBuilder.DropIndex(
                name: "IX_Marks_QuestionId",
                table: "Marks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marksheets",
                table: "Marksheets");

            migrationBuilder.DropIndex(
                name: "IX_Marksheets_AdminId",
                table: "Marksheets");

            migrationBuilder.DropColumn(
                name: "MarkerId",
                table: "UserMarksheets");

            migrationBuilder.DropColumn(
                name: "MarkerId",
                table: "Marks");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Marks");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Marksheets");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Marksheets");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Marksheets");

            migrationBuilder.DropColumn(
                name: "TeamNo",
                table: "Marksheets");

            migrationBuilder.RenameTable(
                name: "Marksheets",
                newName: "Marksheet");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserMarksheets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarksheetId",
                table: "Marks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Marks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignmentId",
                table: "Marksheet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marksheet",
                table: "Marksheet",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMarksheets_UserId",
                table: "UserMarksheets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_MarksheetId",
                table: "Marks",
                column: "MarksheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_UserId",
                table: "Marks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Marksheet_AssignmentId",
                table: "Marksheet",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_GroupId",
                table: "Assignments",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Marksheet_MarksheetId",
                table: "Marks",
                column: "MarksheetId",
                principalTable: "Marksheet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_AspNetUsers_UserId",
                table: "Marks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marksheet_Assignments_AssignmentId",
                table: "Marksheet",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMarksheets_Marksheet_MarksheetId",
                table: "UserMarksheets",
                column: "MarksheetId",
                principalTable: "Marksheet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMarksheets_AspNetUsers_UserId",
                table: "UserMarksheets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
