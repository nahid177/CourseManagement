using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Core.Entities;

public class BookCategoryMapping
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; } = default!;

    public int BookCategoryId { get; set; }
    public BookCategory BookCategory { get; set; } = default!;
}