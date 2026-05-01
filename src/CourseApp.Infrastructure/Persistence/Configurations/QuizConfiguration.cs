using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> entity)
    {
        entity.ToTable("quizzes");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        entity.Property(x => x.CourseId)
            .HasColumnName("course_id")
            .IsRequired();

        entity.Property(x => x.LessonId)
            .HasColumnName("lesson_id")
            .IsRequired();

        entity.Property(x => x.TeacherVideoId)
            .HasColumnName("teacher_video_id")
            .IsRequired();

        entity.Property(x => x.TeacherId)
            .HasColumnName("teacher_id")
            .IsRequired();

        entity.Property(x => x.AdminId)
            .HasColumnName("admin_id");

        entity.Property(x => x.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        entity.HasOne(x => x.TeacherVideo)
            .WithMany()
            .HasForeignKey(x => x.TeacherVideoId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(x => x.Questions)
            .WithOne(x => x.Quiz)
            .HasForeignKey(x => x.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(x => x.TeacherVideoId);
    }
}