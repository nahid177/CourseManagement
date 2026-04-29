using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Core.Entities;

public class Course
{
    public int Id { get; set; }

    public int CourseCategoryId { get; set; }
    public CourseCategory CourseCategory { get; set; } = default!;

    public int? TeacherId { get; set; }

    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }

    public string? ThumbnailUrl { get; set; }

    public decimal Price { get; set; }
    public int DurationHours { get; set; }

    public string Level { get; set; } = "Beginner";
    public bool IsPublished { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}