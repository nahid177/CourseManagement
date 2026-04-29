using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> entity)
    {
        entity.ToTable("courses");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        entity.Property(x => x.CourseCategoryId)
            .HasColumnName("course_category_id")
            .IsRequired();

        entity.Property(x => x.TeacherId)
            .HasColumnName("teacher_id");

        entity.Property(x => x.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(x => x.Slug)
            .HasColumnName("slug")
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("text");

        entity.Property(x => x.ThumbnailUrl)
            .HasColumnName("thumbnail_url")
            .HasMaxLength(500);

        entity.Property(x => x.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        entity.Property(x => x.DurationHours)
            .HasColumnName("duration_hours")
            .IsRequired();

        entity.Property(x => x.Level)
            .HasColumnName("level")
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.IsPublished)
            .HasColumnName("is_published")
            .IsRequired();

        entity.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        entity.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");

        entity.HasIndex(x => x.Slug).IsUnique();
    }
}