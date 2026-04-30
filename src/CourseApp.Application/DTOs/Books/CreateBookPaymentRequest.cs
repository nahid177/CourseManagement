using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Books;

public class CreateBookPaymentRequest
{
    public int BookOrderId { get; set; }
    public string PaymentMethod { get; set; } = default!;
    public string TransactionId { get; set; } = default!;
}