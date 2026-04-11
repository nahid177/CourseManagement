using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Students;

public class StudentLoginRequest
{
    public string Number { get; set; } = default!;
    public string Password { get; set; } = default!;
}