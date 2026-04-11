using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Core.Entities;

public class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Number { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public string? ResetCode { get; set; }
    public DateTime? ResetCodeExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<StudentEnrollment> Enrollments { get; set; } = new List<StudentEnrollment>();
}