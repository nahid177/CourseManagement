using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Books;

public class CreateBookOrderRequest
{
    public int? StudentId { get; set; }

    public string CustomerName { get; set; } = default!;
    public string CustomerEmail { get; set; } = default!;
    public string CustomerNumber { get; set; } = default!;
    public string? DeliveryAddress { get; set; }

    public List<CreateBookOrderItemRequest> Items { get; set; } = new();
}