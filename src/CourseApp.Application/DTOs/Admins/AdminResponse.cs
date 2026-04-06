using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.Admins;

public class AdminResponse
{
    public int Id { get; set; }
    public string Code { get; set; } = default!;
    public string DriveNumber { get; set; } = default!;
    public bool MustResetPassword { get; set; }
    public DateTime CreatedAt { get; set; }
}