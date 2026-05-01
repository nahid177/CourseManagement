using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.TeacherStatuses;

public class CreateTeacherStatusCourseRequest
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = default!;

    public List<CreateTeacherStatusLessonRequest> Lessons { get; set; } = new();
}