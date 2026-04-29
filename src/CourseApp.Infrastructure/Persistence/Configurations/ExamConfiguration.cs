using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class ExamConfiguration : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> entity)
    {
        entity.ToTable("exams");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        entity.Property(x => x.CourseId).HasColumnName("course_id");
        entity.Property(x => x.LessonId).HasColumnName("lesson_id");
        entity.Property(x => x.UserId).HasColumnName("user_id");
        entity.Property(x => x.TeacherId).HasColumnName("teacher_id");

        entity.Property(x => x.ExamUrl)
            .HasColumnName("exam_url")
            .HasMaxLength(500)
            .IsRequired();

        entity.Property(x => x.ExamDetail)
            .HasColumnName("exam_detail")
            .HasColumnType("text");

        entity.Property(x => x.CreatedAt)
            .HasColumnName("created_at");

        entity.HasMany(x => x.Submissions)
            .WithOne(x => x.Exam)
            .HasForeignKey(x => x.ExamId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}