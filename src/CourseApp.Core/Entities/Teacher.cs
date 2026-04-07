using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseApp.Core.Entities;

public class Teacher
{
    public int Id { get; set; }
    public string CodeNumber { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string Position { get; set; } = default!;
    public string Detail { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Type { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}