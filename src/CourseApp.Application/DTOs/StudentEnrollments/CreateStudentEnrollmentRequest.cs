using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.DTOs.StudentEnrollments;

public class CreateStudentEnrollmentRequest
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public string PaymentId { get; set; } = default!;
}