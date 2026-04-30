using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class BookCategoryMappingConfiguration : IEntityTypeConfiguration<BookCategoryMapping>
{
    public void Configure(EntityTypeBuilder<BookCategoryMapping> entity)
    {
        entity.ToTable("book_category_mappings");

        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).UseIdentityAlwaysColumn();

        entity.Property(x => x.BookId)
            .HasColumnName("book_id")
            .IsRequired();

        entity.Property(x => x.BookCategoryId)
            .HasColumnName("book_category_id")
            .IsRequired();

        entity.HasOne(x => x.Book)
            .WithMany(x => x.BookCategoryMappings)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(x => x.BookCategory)
            .WithMany(x => x.BookCategoryMappings)
            .HasForeignKey(x => x.BookCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(x => new { x.BookId, x.BookCategoryId }).IsUnique();
    }
}