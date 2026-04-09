using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Core.Entities;

public class TeacherDocument
{
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public int LessonId { get; set; }

    public string PdfFileName { get; set; } = default!;
    public string PdfFilePath { get; set; } = default!;
    public string? ImageFileName { get; set; }
    public string? ImageFilePath { get; set; }
    public string ContentType { get; set; } = default!;
    public long FileSize { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}