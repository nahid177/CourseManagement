using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class BookOrderItemConfiguration : IEntityTypeConfiguration<BookOrderItem>
{
    public void Configure(EntityTypeBuilder<BookOrderItem> entity)
    {
        entity.ToTable("book_order_items");

        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).UseIdentityAlwaysColumn();

        entity.Property(x => x.BookOrderId)
            .HasColumnName("book_order_id")
            .IsRequired();

        entity.Property(x => x.BookId)
            .HasColumnName("book_id")
            .IsRequired();

        entity.Property(x => x.SellType)
            .HasColumnName("sell_type")
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        entity.Property(x => x.UnitPrice)
            .HasColumnName("unit_price")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        entity.Property(x => x.TotalPrice)
            .HasColumnName("total_price")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        entity.HasOne(x => x.Book)
            .WithMany()
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}