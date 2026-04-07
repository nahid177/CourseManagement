using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Teachers;

public class ResetTeacherPasswordRequest
{
    public string NewPassword { get; set; } = default!;
}
