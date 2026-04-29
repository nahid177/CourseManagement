using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourseApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddExamSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    course_id = table.Column<int>(type: "integer", nullable: false),
                    lesson_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    teacher_id = table.Column<int>(type: "integer", nullable: false),
                    exam_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    exam_detail = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "exam_submissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    exam_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    exam_ans_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    answer_detail = table.Column<string>(type: "text", nullable: false),
                    submitted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam_submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_exam_submissions_exams_exam_id",
                        column: x => x.exam_id,
                        principalTable: "exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_exam_submissions_exam_id",
                table: "exam_submissions",
                column: "exam_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exam_submissions");

            migrationBuilder.DropTable(
                name: "exams");
        }
    }
}
