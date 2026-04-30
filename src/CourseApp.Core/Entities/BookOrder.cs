using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Core.Entities;

public class BookOrder
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public string CustomerName { get; set; } = default!;
    public string CustomerEmail { get; set; } = default!;
    public string CustomerNumber { get; set; } = default!;
    public string? DeliveryAddress { get; set; }

    public decimal TotalAmount { get; set; }

    public string OrderStatus { get; set; } = "Pending";
    public string PaymentStatus { get; set; } = "Unpaid";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PaidAt { get; set; }

    public ICollection<BookOrderItem> Items { get; set; } = new List<BookOrderItem>();
}