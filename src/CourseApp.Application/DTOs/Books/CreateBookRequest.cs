using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Books;

public class CreateBookRequest
{
    public List<int> BookCategoryIds { get; set; } = new();

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
}