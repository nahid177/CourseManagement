using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Core.Entities;

public class ExamSubmission
{
    public int Id { get; set; }

    public int ExamId { get; set; }
    public Exam Exam { get; set; } = default!;

    public int UserId { get; set; }

    public string ExamAnsUrl { get; set; } = default!;
    public string AnswerDetail { get; set; } = default!;

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}