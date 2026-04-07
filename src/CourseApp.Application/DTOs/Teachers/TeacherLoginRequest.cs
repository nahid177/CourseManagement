using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Application.DTOs.Teachers;

public class TeacherLoginRequest
{
    public string CodeNumber { get; set; } = default!;
    public string Password { get; set; } = default!;
}