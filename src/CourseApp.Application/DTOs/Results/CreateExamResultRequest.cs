using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Results;

public class CreateExamResultRequest
{
    public int ExamId { get; set; }
    public int UserId { get; set; }
    public int TeacherId { get; set; }
    public int CourseId { get; set; }
    public int LessonId { get; set; }

    public decimal Marks { get; set; }
    public decimal TotalMarks { get; set; }
    public string? TeacherFeedback { get; set; }
}