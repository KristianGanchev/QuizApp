using Microsoft.EntityFrameworkCore.Migrations;

namespace Quizler.Data.Migrations
{
    public partial class AddSelectedAnswersToResultTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Answers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ResultId",
                table: "Answers",
                column: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Results_ResultId",
                table: "Answers",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Results_ResultId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_ResultId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Answers");
        }
    }
}
