using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> entity)
    {
        entity.ToTable("teachers");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        entity.Property(x => x.CodeNumber)
            .HasColumnName("code_number")
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(x => x.Location)
            .HasColumnName("location")
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(x => x.Position)
            .HasColumnName("position")
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(x => x.Detail)
            .HasColumnName("detail")
            .HasColumnType("text")
            .IsRequired();

        entity.Property(x => x.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired();

        entity.Property(x => x.Type)
            .HasColumnName("type")
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        entity.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");

        entity.HasIndex(x => x.CodeNumber).IsUnique();
    }
}