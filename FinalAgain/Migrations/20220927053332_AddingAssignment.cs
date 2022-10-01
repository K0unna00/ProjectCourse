using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalAgain.Migrations
{
    public partial class AddingAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropColumn(
                name: "TeamNo",
                table: "Marksheets");

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamNo = table.Column<int>(nullable: false),
                    StudentName1 = table.Column<string>(nullable: true),
                    StudentName2 = table.Column<string>(nullable: true),
                    StudentName3 = table.Column<string>(nullable: true),
                    StudentName4 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamMakrsheets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(nullable: false),
                    MarksheetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMakrsheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamMakrsheets_Marksheets_MarksheetId",
                        column: x => x.MarksheetId,
                        principalTable: "Marksheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMakrsheets_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMakrsheets_MarksheetId",
                table: "TeamMakrsheets",
                column: "MarksheetId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMakrsheets_TeamId",
                table: "TeamMakrsheets",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMakrsheets");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "TeamNo",
                table: "Marksheets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarksheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_Students_MarksheetId",
                table: "Students",
                column: "MarksheetId");
        }
    }
}
