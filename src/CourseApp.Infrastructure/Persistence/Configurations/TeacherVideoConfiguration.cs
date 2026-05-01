using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class TeacherVideoConfiguration : IEntityTypeConfiguration<TeacherVideo>
{
    public void Configure(EntityTypeBuilder<TeacherVideo> entity)
    {
        entity.ToTable("teacher_videos");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        entity.Property(x => x.CourseId)
            .HasColumnName("course_id")
            .IsRequired();

        entity.Property(x => x.TeacherId)
            .HasColumnName("teacher_id")
            .IsRequired();

        entity.Property(x => x.LessonId)
            .HasColumnName("lesson_id")
            .IsRequired();

        entity.Property(x => x.ClassName)
            .HasColumnName("class_name")
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(x => x.ClassDetail)
            .HasColumnName("class_detail")
            .HasColumnType("text");

        entity.Property(x => x.VideoName)
            .HasColumnName("video_name")
            .HasMaxLength(255)
            .IsRequired();

        entity.Property(x => x.OriginalFileName)
            .HasColumnName("original_file_name")
            .HasMaxLength(255)
            .IsRequired();

        entity.Property(x => x.VideoFilePath)
            .HasColumnName("video_file_path")
            .HasMaxLength(500)
            .IsRequired();

        entity.Property(x => x.ContentType)
            .HasColumnName("content_type")
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(x => x.FileSize)
            .HasColumnName("file_size")
            .IsRequired();

        entity.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        entity.HasIndex(x => x.CourseId);
        entity.HasIndex(x => x.TeacherId);
        entity.HasIndex(x => x.LessonId);
    }
}