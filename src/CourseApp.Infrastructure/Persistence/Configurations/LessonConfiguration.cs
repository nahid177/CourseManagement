using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> entity)
    {
        entity.ToTable("lessons");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        entity.Property(x => x.CourseId)
            .HasColumnName("course_id")
            .IsRequired();

        entity.Property(x => x.TeacherId)
            .HasColumnName("teacher_id")
            .IsRequired();

        entity.Property(x => x.LessonNumber)
            .HasColumnName("lesson_number")
            .IsRequired();

        entity.Property(x => x.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        // Optional index
        entity.HasIndex(x => new { x.CourseId, x.LessonNumber }).IsUnique();
    }
}