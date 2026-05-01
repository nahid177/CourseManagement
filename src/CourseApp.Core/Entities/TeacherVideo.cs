using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Core.Entities;

public class TeacherVideo
{
    public int Id { get; set; }

    public int CourseId { get; set; }
    public int TeacherId { get; set; }
    public int LessonId { get; set; }

    public string ClassName { get; set; } = default!;
    public string? ClassDetail { get; set; }

    public string VideoName { get; set; } = default!;
    public string OriginalFileName { get; set; } = default!;
    public string VideoFilePath { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public long FileSize { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}