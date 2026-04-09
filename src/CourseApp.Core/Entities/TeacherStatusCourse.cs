using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Core.Entities;

public class TeacherStatusCourse
{
    public int Id { get; set; }

    public int TeacherStatusId { get; set; }
    public TeacherStatus TeacherStatus { get; set; } = default!;

    public int CourseId { get; set; }
    public string CourseName { get; set; } = default!;

    public ICollection<TeacherStatusLesson> Lessons { get; set; } = new List<TeacherStatusLesson>();
}