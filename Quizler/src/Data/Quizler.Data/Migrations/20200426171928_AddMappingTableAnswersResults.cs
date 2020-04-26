using Microsoft.EntityFrameworkCore.Migrations;

namespace Quizler.Data.Migrations
{
    public partial class AddMappingTableAnswersResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "AnswersResults",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false),
                    ResultId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersResults", x => new { x.AnswerId, x.ResultId });
                    table.ForeignKey(
                        name: "FK_AnswersResults_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswersResults_Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswersResults_ResultId",
                table: "AnswersResults",
                column: "ResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswersResults");

            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Answers",
                type: "int",
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
    }
}
