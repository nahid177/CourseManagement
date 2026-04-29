using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Exams
{
    public class CreateExamSubmissionRequest
    {
        public int ExamId { get; set; }
        public int UserId { get; set; }

        public string ExamAnsUrl { get; set; } = default!;
        public string AnswerDetail { get; set; } = default!;
    }
}
