using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Application.DTOs.Results;

public class CreateQuizResultRequest
{
    public int QuizId { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public int LessonNumber { get; set; }

    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
}