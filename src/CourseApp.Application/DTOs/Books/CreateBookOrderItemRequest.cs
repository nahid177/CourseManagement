using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Books;

public class CreateBookOrderItemRequest
{
    public int BookId { get; set; }
    public string SellType { get; set; } = default!; // Physical / Pdf
    public int Quantity { get; set; }
}
