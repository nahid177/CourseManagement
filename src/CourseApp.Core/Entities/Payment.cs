using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Core.Entities;

public class Payment
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; } = default!;

    public int CourseId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = default!; // bkash / nagad / card
    public string TransactionId { get; set; } = default!;

    public string Status { get; set; } = "Pending"; // Pending, Success, Failed

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PaidAt { get; set; }
}