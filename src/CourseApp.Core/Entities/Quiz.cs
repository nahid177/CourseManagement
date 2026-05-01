using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Core.Entities;

public class Quiz
{
    public int Id { get; set; }

    public int CourseId { get; set; }
    public int LessonId { get; set; }

    public int TeacherVideoId { get; set; }
    public TeacherVideo TeacherVideo { get; set; } = default!;

    public int TeacherId { get; set; }
    public int? AdminId { get; set; }

    public string Title { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
}