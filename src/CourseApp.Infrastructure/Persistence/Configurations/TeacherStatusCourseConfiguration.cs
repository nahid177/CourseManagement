using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class TeacherStatusCourseConfiguration : IEntityTypeConfiguration<TeacherStatusCourse>
{
    public void Configure(EntityTypeBuilder<TeacherStatusCourse> entity)
    {
        entity.ToTable("teacher_status_courses");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        entity.Property(x => x.TeacherStatusId)
            .HasColumnName("teacher_status_id")
            .IsRequired();

        entity.Property(x => x.CourseId)
            .HasColumnName("course_id")
            .IsRequired();

        entity.Property(x => x.CourseName)
            .HasColumnName("course_name")
            .HasMaxLength(200)
            .IsRequired();

        entity.HasMany(x => x.Lessons)
            .WithOne(x => x.TeacherStatusCourse)
            .HasForeignKey(x => x.TeacherStatusCourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}