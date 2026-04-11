using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Application.DTOs.Students;

public class RequestStudentResetCodeRequest
{
    public string Email { get; set; } = default!;
}