using CourseApp.Application.DTOs.Students;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;
    private readonly IPasswordHasher _passwordHasher;

    public StudentsController(
        IStudentRepository studentRepository,
        IPasswordHasher passwordHasher)
    {
        _studentRepository = studentRepository;
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var students = await _studentRepository.GetAllAsync(cancellationToken);

        var response = students.Select(x => new StudentResponse
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email,
            Number = x.Number,
            CreatedAt = x.CreatedAt
        });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateStudentRequest request,
        CancellationToken cancellationToken)
    {
        var existingEmail = await _studentRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingEmail is not null)
        {
            return BadRequest(new { message = "Email already exists." });
        }

        var existingNumber = await _studentRepository.GetByNumberAsync(request.Number, cancellationToken);
        if (existingNumber is not null)
        {
            return BadRequest(new { message = "Number already exists." });
        }

        var student = new Student
        {
            Name = request.Name,
            Email = request.Email,
            Number = request.Number,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        await _studentRepository.AddAsync(student, cancellationToken);
        await _studentRepository.SaveChangesAsync(cancellationToken);

        return Ok(new StudentResponse
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Number = student.Number,
            CreatedAt = student.CreatedAt
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] StudentLoginRequest request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByNumberAsync(request.Number, cancellationToken);

        if (student is null)
        {
            return Unauthorized(new { message = "Invalid number or password." });
        }

        var isValid = _passwordHasher.VerifyPassword(request.Password, student.PasswordHash);

        if (!isValid)
        {
            return Unauthorized(new { message = "Invalid number or password." });
        }

        return Ok(new
        {
            message = "Login successful.",
            student = new StudentResponse
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Number = student.Number,
                CreatedAt = student.CreatedAt
            }
        });
    }

    [HttpPost("request-reset-code")]
    public async Task<IActionResult> RequestResetCode(
        [FromBody] RequestStudentResetCodeRequest request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (student is null)
        {
            return NotFound(new { message = "Student not found." });
        }

        var resetCode = Random.Shared.Next(100000, 999999).ToString();

        student.ResetCode = resetCode;
        student.ResetCodeExpiresAt = DateTime.UtcNow.AddMinutes(10);
        student.UpdatedAt = DateTime.UtcNow;

        await _studentRepository.SaveChangesAsync(cancellationToken);

        return Ok(new
        {
            message = "Reset code generated.",
            resetCode,
            email = student.Email
        });
    }

    [HttpPost("confirm-reset-code")]
    public async Task<IActionResult> ConfirmResetCode(
        [FromBody] ConfirmStudentResetCodeRequest request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByResetCodeAsync(request.ResetCode, cancellationToken);
        if (student is null)
        {
            return BadRequest(new { message = "Invalid reset code." });
        }

        if (!student.ResetCodeExpiresAt.HasValue || student.ResetCodeExpiresAt.Value < DateTime.UtcNow)
        {
            return BadRequest(new { message = "Reset code expired." });
        }

        student.PasswordHash = _passwordHasher.HashPassword(request.NewPassword);
        student.ResetCode = null;
        student.ResetCodeExpiresAt = null;
        student.UpdatedAt = DateTime.UtcNow;

        await _studentRepository.SaveChangesAsync(cancellationToken);

        return Ok(new { message = "Password reset successful." });
    }
}