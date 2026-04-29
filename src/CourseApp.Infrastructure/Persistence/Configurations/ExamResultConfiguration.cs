using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class ExamResultConfiguration : IEntityTypeConfiguration<ExamResult>
{
    public void Configure(EntityTypeBuilder<ExamResult> entity)
    {
        entity.ToTable("exam_results");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id).UseIdentityAlwaysColumn();

        entity.Property(x => x.ExamId).HasColumnName("exam_id").IsRequired();
        entity.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
        entity.Property(x => x.TeacherId).HasColumnName("teacher_id").IsRequired();
        entity.Property(x => x.CourseId).HasColumnName("course_id").IsRequired();
        entity.Property(x => x.LessonId).HasColumnName("lesson_id").IsRequired();

        entity.Property(x => x.Marks)
            .HasColumnName("marks")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        entity.Property(x => x.TotalMarks)
            .HasColumnName("total_marks")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        entity.Property(x => x.IsPassed).HasColumnName("is_passed").IsRequired();

        entity.Property(x => x.TeacherFeedback)
            .HasColumnName("teacher_feedback")
            .HasColumnType("text");

        entity.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
    }
}