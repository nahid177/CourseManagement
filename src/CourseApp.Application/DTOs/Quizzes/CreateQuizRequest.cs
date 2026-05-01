using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CourseApp.Application.DTOs.Quizzes;

public class CreateQuizRequest
{
    public int TeacherVideoId { get; set; }
    public int? AdminId { get; set; }

    public string Title { get; set; } = default!;

    public List<CreateQuizQuestionRequest> Questions { get; set; } = new();
}