using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiTest.Migrations
{
    public partial class tasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Task = table.Column<int>(nullable: false),
                    Question = table.Column<int>(nullable: false),
                    CorrectAnswer = table.Column<int>(nullable: false),
                    StudentAnswer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAnswers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User = table.Column<int>(nullable: false),
                    Test = table.Column<int>(nullable: false),
                    Sum = table.Column<int>(nullable: false),
                    Pass = table.Column<int>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    Finish = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskAnswers");

            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
