using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Application.DTOs.TeacherStatuses;

public class CreateTeacherStatusRequest
{
    public string TeacherCode { get; set; } = default!;
    public string? ImageUrl { get; set; }

    public List<CreateTeacherStatusCourseRequest> Courses { get; set; } = new();
}