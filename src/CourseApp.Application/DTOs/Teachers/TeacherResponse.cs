using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Application.DTOs.Teachers;

public class TeacherResponse
{
    public int Id { get; set; }
    public string CodeNumber { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string Position { get; set; } = default!;
    public string Detail { get; set; } = default!;
    public string Type { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}