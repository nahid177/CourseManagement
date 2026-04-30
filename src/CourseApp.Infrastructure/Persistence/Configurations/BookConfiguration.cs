using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> entity)
    {
        entity.ToTable("books");

        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).UseIdentityAlwaysColumn();

        entity.Property(x => x.Title)
            .HasColumnName("title")
            .HasMaxLength(250)
            .IsRequired();

        entity.Property(x => x.Slug)
            .HasColumnName("slug")
            .HasMaxLength(250)
            .IsRequired();

        entity.Property(x => x.Author)
            .HasColumnName("author")
            .HasMaxLength(200);

        entity.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("text");

        entity.Property(x => x.CoverImageUrl)
            .HasColumnName("cover_image_url")
            .HasMaxLength(500);

        entity.Property(x => x.IsPhysicalAvailable)
            .HasColumnName("is_physical_available")
            .IsRequired();

        entity.Property(x => x.PhysicalPrice)
            .HasColumnName("physical_price")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        entity.Property(x => x.StockQuantity)
            .HasColumnName("stock_quantity")
            .IsRequired();

        entity.Property(x => x.IsPdfAvailable)
            .HasColumnName("is_pdf_available")
            .IsRequired();

        entity.Property(x => x.PdfPrice)
            .HasColumnName("pdf_price")
            .HasColumnType("decimal(10,2)");

        entity.Property(x => x.PdfFileUrl)
            .HasColumnName("pdf_file_url")
            .HasMaxLength(500);

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