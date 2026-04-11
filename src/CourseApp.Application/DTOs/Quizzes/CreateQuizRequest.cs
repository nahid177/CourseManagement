using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Quizzes;

public class CreateQuizRequest
{
    public int CourseId { get; set; }
    public int LessonNumber { get; set; }
    public string VideoId { get; set; } = default!;

    public int TeacherId { get; set; }
    public int? AdminId { get; set; }

    public string Title { get; set; } = default!;

    public List<CreateQuizQuestionRequest> Questions { get; set; } = new();
}