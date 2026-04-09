using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.TeacherFiles;

public class TeacherDocumentUploadRequest
{
    public int TeacherId { get; set; }
    public int LessonId { get; set; }
}