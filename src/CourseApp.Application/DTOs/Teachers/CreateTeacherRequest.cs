using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Application.DTOs.Teachers;

public class CreateTeacherRequest
{
    public string CodeNumber { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string Position { get; set; } = default!;
    public string Detail { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Type { get; set; } = default!;
}