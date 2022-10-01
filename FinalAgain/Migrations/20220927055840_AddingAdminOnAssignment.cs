using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalAgain.Migrations
{
    public partial class AddingAdminOnAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Assignments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AdminId",
                table: "Assignments",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AspNetUsers_AdminId",
                table: "Assignments",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AspNetUsers_AdminId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_AdminId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Assignments");
        }
    }
}
