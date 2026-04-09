using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourseApp.Infrastructure.Persistence.Migrations
{
    public partial class AddTeacherStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "teacher_statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    teacher_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    admin_approved = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "teacher_status_courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    teacher_status_id = table.Column<int>(type: "integer", nullable: false),
                    course_id = table.Column<int>(type: "integer", nullable: false),
                    course_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_status_courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_status_courses_teacher_statuses_teacher_status_id",
                        column: x => x.teacher_status_id,
                        principalTable: "teacher_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teacher_status_lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    teacher_status_course_id = table.Column<int>(type: "integer", nullable: false),
                    lesson_number = table.Column<int>(type: "integer", nullable: false),
                    video_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    pdf_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_status_lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_status_lessons_teacher_status_courses_teacher_status_course_id",
                        column: x => x.teacher_status_course_id,
                        principalTable: "teacher_status_courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_teacher_status_courses_teacher_status_id",
                table: "teacher_status_courses",
                column: "teacher_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_status_lessons_teacher_status_course_id",
                table: "teacher_status_lessons",
                column: "teacher_status_course_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "teacher_status_lessons");

            migrationBuilder.DropTable(
                name: "teacher_status_courses");

            migrationBuilder.DropTable(
                name: "teacher_statuses");
        }
    }
}