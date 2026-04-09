using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class TeacherStatusLessonConfiguration : IEntityTypeConfiguration<TeacherStatusLesson>
{
    public void Configure(EntityTypeBuilder<TeacherStatusLesson> entity)
    {
        entity.ToTable("teacher_status_lessons");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        entity.Property(x => x.TeacherStatusCourseId)
            .HasColumnName("teacher_status_course_id")
            .IsRequired();

        entity.Property(x => x.LessonNumber)
            .HasColumnName("lesson_number")
            .IsRequired();

        entity.Property(x => x.VideoId)
            .HasColumnName("video_id")
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(x => x.PdfId)
            .HasColumnName("pdf_id")
            .HasMaxLength(100)
            .IsRequired();
    }
}
