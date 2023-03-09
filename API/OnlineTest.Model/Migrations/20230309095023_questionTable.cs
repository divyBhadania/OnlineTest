using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTest.Migrations
{
    /// <inheritdoc />
    public partial class questionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tests_Technologies_TechnologyId",
                table: "tests");

            migrationBuilder.DropForeignKey(
                name: "FK_tests_Users_CreatedBy",
                table: "tests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tests",
                table: "tests");

            migrationBuilder.RenameTable(
                name: "tests",
                newName: "Tests");

            migrationBuilder.RenameIndex(
                name: "IX_tests_TechnologyId",
                table: "Tests",
                newName: "IX_Tests_TechnologyId");

            migrationBuilder.RenameIndex(
                name: "IX_tests_CreatedBy",
                table: "Tests",
                newName: "IX_Tests_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tests",
                table: "Tests",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Que = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Weightage = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestId",
                table: "Questions",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Technologies_TechnologyId",
                table: "Tests",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Users_CreatedBy",
                table: "Tests",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Technologies_TechnologyId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Users_CreatedBy",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tests",
                table: "Tests");

            migrationBuilder.RenameTable(
                name: "Tests",
                newName: "tests");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_TechnologyId",
                table: "tests",
                newName: "IX_tests_TechnologyId");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_CreatedBy",
                table: "tests",
                newName: "IX_tests_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tests",
                table: "tests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tests_Technologies_TechnologyId",
                table: "tests",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tests_Users_CreatedBy",
                table: "tests",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
