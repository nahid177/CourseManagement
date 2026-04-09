using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Core.Entities;

public class TeacherStatus
{
    public int Id { get; set; }
    public string TeacherCode { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public bool AdminApproved { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<TeacherStatusCourse> Courses { get; set; } = new List<TeacherStatusCourse>();
}