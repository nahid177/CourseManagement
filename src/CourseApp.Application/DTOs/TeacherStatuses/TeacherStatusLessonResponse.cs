namespace CourseApp.Application.DTOs.TeacherStatuses;

public class TeacherStatusLessonResponse
{
    public int Id { get; set; }
    public int LessonNumber { get; set; }
    public string VideoId { get; set; } = default!;
    public string PdfId { get; set; } = default!;
}
