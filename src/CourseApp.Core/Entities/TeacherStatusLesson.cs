using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Core.Entities;

public class TeacherStatusLesson
{
    public int Id { get; set; }

    public int TeacherStatusCourseId { get; set; }
    public TeacherStatusCourse TeacherStatusCourse { get; set; } = default!;

    public int LessonNumber { get; set; }
    public string VideoId { get; set; } = default!;
    public string PdfId { get; set; } = default!;
}