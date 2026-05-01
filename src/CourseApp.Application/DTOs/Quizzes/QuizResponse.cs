using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Quizzes;

public class QuizResponse
{
    public int Id { get; set; }

    public int CourseId { get; set; }
    public int LessonId { get; set; }
    public int TeacherVideoId { get; set; }

    public int TeacherId { get; set; }
    public int? AdminId { get; set; }

    public string Title { get; set; } = default!;
    public DateTime CreatedAt { get; set; }

    public List<QuizQuestionResponse> Questions { get; set; } = new();
}