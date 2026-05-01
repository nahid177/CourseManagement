using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTeacherVideoClassFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "class_detail",
                table: "teacher_videos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "class_name",
                table: "teacher_videos",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "course_id",
                table: "teacher_videos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "original_file_name",
                table: "teacher_videos",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_videos_course_id",
                table: "teacher_videos",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_videos_lesson_id",
                table: "teacher_videos",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_videos_teacher_id",
                table: "teacher_videos",
                column: "teacher_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_teacher_videos_course_id",
                table: "teacher_videos");

            migrationBuilder.DropIndex(
                name: "IX_teacher_videos_lesson_id",
                table: "teacher_videos");

            migrationBuilder.DropIndex(
                name: "IX_teacher_videos_teacher_id",
                table: "teacher_videos");

            migrationBuilder.DropColumn(
                name: "class_detail",
                table: "teacher_videos");

            migrationBuilder.DropColumn(
                name: "class_name",
                table: "teacher_videos");

            migrationBuilder.DropColumn(
                name: "course_id",
                table: "teacher_videos");

            migrationBuilder.DropColumn(
                name: "original_file_name",
                table: "teacher_videos");
        }
    }
}
