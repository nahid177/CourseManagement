using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Core.Entities;

public class Exam
{
    public int Id { get; set; }

    public int CourseId { get; set; }
    public int LessonId { get; set; }

    public int UserId { get; set; }
    public int TeacherId { get; set; }

    public string ExamUrl { get; set; } = default!;
    public string ExamDetail { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ExamSubmission> Submissions { get; set; } = new List<ExamSubmission>();
}