using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Admins;

public class CreateAdminRequest
{
    public string Code { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string DriveNumber { get; set; } = default!;
}