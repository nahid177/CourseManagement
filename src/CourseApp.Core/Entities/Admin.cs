using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Core.Entities;

public class Admin
{
    public int Id { get; set; }

    public string Code { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;

    public string DriveNumber { get; set; } = default!;

    public bool MustResetPassword { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
