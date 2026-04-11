using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Core.Entities;

public class Lesson
{
    public int Id { get; set; }

    public int CourseId { get; set; }
    public int TeacherId { get; set; }

    public int LessonNumber { get; set; } // e.g. Lesson 1, Lesson 2
    public string Title { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}