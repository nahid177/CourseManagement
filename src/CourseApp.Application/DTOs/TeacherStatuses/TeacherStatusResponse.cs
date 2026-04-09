using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.TeacherStatuses;

public class TeacherStatusResponse
{
    public int Id { get; set; }
    public string TeacherCode { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public bool AdminApproved { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<TeacherStatusCourseResponse> Courses { get; set; } = new();
}