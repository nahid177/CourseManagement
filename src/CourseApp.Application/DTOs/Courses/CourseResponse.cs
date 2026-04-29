using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Courses;

public class CourseResponse
{
    public int Id { get; set; }

    public int CourseCategoryId { get; set; }
    public string CategoryName { get; set; } = default!;

    public int? TeacherId { get; set; }

    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }

    public string? ThumbnailUrl { get; set; }

    public decimal Price { get; set; }
    public int DurationHours { get; set; }

    public string Level { get; set; } = default!;
    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; set; }
}