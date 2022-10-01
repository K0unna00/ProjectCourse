using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalAgain.Migrations
{
    public partial class AddingTeamIdOnMark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Marks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Marks_TeamId",
                table: "Marks",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Teams_TeamId",
                table: "Marks",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Teams_TeamId",
                table: "Marks");

            migrationBuilder.DropIndex(
                name: "IX_Marks_TeamId",
                table: "Marks");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Marks");
        }
    }
}
