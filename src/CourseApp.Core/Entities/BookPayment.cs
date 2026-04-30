using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Core.Entities;

public class BookPayment
{
    public int Id { get; set; }

    public int BookOrderId { get; set; }
    public BookOrder BookOrder { get; set; } = default!;

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = default!;
    public string TransactionId { get; set; } = default!;
    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PaidAt { get; set; }
}