using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Core.Entities;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Author { get; set; }
    public string? Description { get; set; }

    public string? CoverImageUrl { get; set; }

    public bool IsPhysicalAvailable { get; set; }
    public decimal PhysicalPrice { get; set; }
    public int StockQuantity { get; set; }

    public bool IsPdfAvailable { get; set; }
    public decimal? PdfPrice { get; set; }
    public string? PdfFileUrl { get; set; }

    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<BookCategoryMapping> BookCategoryMappings { get; set; } = new List<BookCategoryMapping>();
}