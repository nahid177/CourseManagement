using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class TeacherStatusConfiguration : IEntityTypeConfiguration<TeacherStatus>
{
    public void Configure(EntityTypeBuilder<TeacherStatus> entity)
    {
        entity.ToTable("teacher_statuses");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        entity.Property(x => x.TeacherCode)
            .HasColumnName("teacher_code")
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.ImageUrl)
            .HasColumnName("image_url")
            .HasMaxLength(500);

        entity.Property(x => x.AdminApproved)
            .HasColumnName("admin_approved")
            .IsRequired();

        entity.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        entity.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");

        entity.HasMany(x => x.Courses)
            .WithOne(x => x.TeacherStatus)
            .HasForeignKey(x => x.TeacherStatusId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}