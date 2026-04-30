using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class BookPaymentConfiguration : IEntityTypeConfiguration<BookPayment>
{
    public void Configure(EntityTypeBuilder<BookPayment> entity)
    {
        entity.ToTable("book_payments");

        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).UseIdentityAlwaysColumn();

        entity.Property(x => x.BookOrderId)
            .HasColumnName("book_order_id")
            .IsRequired();

        entity.Property(x => x.Amount)
            .HasColumnName("amount")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        entity.Property(x => x.PaymentMethod)
            .HasColumnName("payment_method")
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.TransactionId)
            .HasColumnName("transaction_id")
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(x => x.Status)
            .HasColumnName("status")
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        entity.Property(x => x.PaidAt)
            .HasColumnName("paid_at");

        entity.HasIndex(x => x.TransactionId).IsUnique();

        entity.HasOne(x => x.BookOrder)
            .WithMany()
            .HasForeignKey(x => x.BookOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}