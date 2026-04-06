using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Admins;

public class LoginRequest
{
    public string Code { get; set; } = default!;
    public string Password { get; set; } = default!;
}