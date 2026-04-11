using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Quizzes;

public class QuizQuestionResponse
{
    public int Id { get; set; }
    public string Question { get; set; } = default!;
    public string OptionA { get; set; } = default!;
    public string OptionB { get; set; } = default!;
    public string OptionC { get; set; } = default!;
    public string OptionD { get; set; } = default!;
    public string CorrectAnswer { get; set; } = default!;
}