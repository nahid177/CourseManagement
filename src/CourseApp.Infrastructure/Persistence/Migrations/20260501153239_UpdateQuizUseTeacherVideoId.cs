using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuizUseTeacherVideoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "video_id",
                table: "quizzes");

            migrationBuilder.RenameColumn(
                name: "lesson_number",
                table: "quizzes",
                newName: "teacher_video_id");

            migrationBuilder.AddColumn<int>(
                name: "lesson_id",
                table: "quizzes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_quizzes_teacher_video_id",
                table: "quizzes",
                column: "teacher_video_id");

            migrationBuilder.AddForeignKey(
                name: "FK_quizzes_teacher_videos_teacher_video_id",
                table: "quizzes",
                column: "teacher_video_id",
                principalTable: "teacher_videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quizzes_teacher_videos_teacher_video_id",
                table: "quizzes");

            migrationBuilder.DropIndex(
                name: "IX_quizzes_teacher_video_id",
                table: "quizzes");

            migrationBuilder.DropColumn(
                name: "lesson_id",
                table: "quizzes");

            migrationBuilder.RenameColumn(
                name: "teacher_video_id",
                table: "quizzes",
                newName: "lesson_number");

            migrationBuilder.AddColumn<string>(
                name: "video_id",
                table: "quizzes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
