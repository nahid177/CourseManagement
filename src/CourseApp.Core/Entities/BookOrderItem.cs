using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Core.Entities;

public class BookOrderItem
{
    public int Id { get; set; }

    public int BookOrderId { get; set; }
    public BookOrder BookOrder { get; set; } = default!;

    public int BookId { get; set; }
    public Book Book { get; set; } = default!;

    public string SellType { get; set; } = default!; // Physical / Pdf

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}