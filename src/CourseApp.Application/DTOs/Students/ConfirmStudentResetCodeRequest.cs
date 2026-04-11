using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Students;

public class ConfirmStudentResetCodeRequest
{
    public string ResetCode { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}