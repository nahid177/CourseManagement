using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseApp.Infrastructure.Persistence.Migrations
{
    public partial class AddVideoNameToTeacherVideos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "video_name",
                table: "teacher_videos",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: ""
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "video_name",
                table: "teacher_videos"
            );
        }
    }
}