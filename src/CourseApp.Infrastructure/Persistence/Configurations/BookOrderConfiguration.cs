using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class BookOrderConfiguration : IEntityTypeConfiguration<BookOrder>
{
    public void Configure(EntityTypeBuilder<BookOrder> entity)
    {
        entity.ToTable("book_orders");

        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).UseIdentityAlwaysColumn();

        entity.Property(x => x.StudentId)
            .HasColumnName("student_id");

        entity.Property(x => x.CustomerName)
            .HasColumnName("customer_name")
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(x => x.CustomerEmail)
            .HasColumnName("customer_email")
            .HasMaxLength(150)
            .IsRequired();

        entity.Property(x => x.CustomerNumber)
            .HasColumnName("customer_number")
            .HasMaxLength(30)
            .IsRequired();

        entity.Property(x => x.DeliveryAddress)
            .HasColumnName("delivery_address")
            .HasColumnType("text");

        entity.Property(x => x.TotalAmount)
            .HasColumnName("total_amount")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        entity.Property(x => x.OrderStatus)
            .HasColumnName("order_status")
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.PaymentStatus)
            .HasColumnName("payment_status")
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        entity.Property(x => x.PaidAt)
            .HasColumnName("paid_at");

        entity.HasMany(x => x.Items)
            .WithOne(x => x.BookOrder)
            .HasForeignKey(x => x.BookOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}