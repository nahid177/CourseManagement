using CourseApp.Application.DTOs.StudentEnrollments;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using CourseApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentEnrollmentsController : ControllerBase
{
    private readonly IStudentEnrollmentRepository _enrollmentRepository;
    private readonly IStudentRepository _studentRepository;

    public StudentEnrollmentsController(
        IStudentEnrollmentRepository enrollmentRepository,
        IStudentRepository studentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _studentRepository = studentRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateStudentEnrollmentRequest request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
        if (student is null)
        {
            return NotFound(new { message = "Student not found." });
        }

        var enrollment = new StudentEnrollment
        {
            StudentId = request.StudentId,
            CourseId = request.CourseId,
            PaymentId = request.PaymentId,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        await _enrollmentRepository.AddAsync(enrollment, cancellationToken);
        await _enrollmentRepository.SaveChangesAsync(cancellationToken);

        return Ok(new StudentEnrollmentResponse
        {
            Id = enrollment.Id,
            StudentId = enrollment.StudentId,
            CourseId = enrollment.CourseId,
            PaymentId = enrollment.PaymentId,
            Status = enrollment.Status,
            CreatedAt = enrollment.CreatedAt
        });
    }

    [HttpGet("student/{studentId:int}")]
    public async Task<IActionResult> GetByStudentId(int studentId, CancellationToken cancellationToken)
    {
        var data = await _enrollmentRepository.GetByStudentIdAsync(studentId, cancellationToken);

        var response = data.Select(x => new StudentEnrollmentResponse
        {
            Id = x.Id,
            StudentId = x.StudentId,
            CourseId = x.CourseId,
            PaymentId = x.PaymentId,
            Status = x.Status,
            CreatedAt = x.CreatedAt
        });

        return Ok(response);
    }
}