using CourseApp.Application.DTOs.Teachers;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public TeachersController(
        ITeacherRepository teacherRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _teacherRepository = teacherRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var teachers = await _teacherRepository.GetAllAsync(cancellationToken);

        var response = teachers.Select(x => new TeacherResponse
        {
            Id = x.Id,
            CodeNumber = x.CodeNumber,
            Name = x.Name,
            Location = x.Location,
            Position = x.Position,
            Detail = x.Detail,
            Type = x.Type,
            CreatedAt = x.CreatedAt
        });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTeacherRequest request,
        CancellationToken cancellationToken)
    {
        var existingTeacher = await _teacherRepository.GetByCodeNumberAsync(request.CodeNumber, cancellationToken);
        if (existingTeacher is not null)
        {
            return BadRequest(new { message = "Teacher code number already exists." });
        }

        var teacher = new Teacher
        {
            CodeNumber = request.CodeNumber,
            Name = request.Name,
            Location = request.Location,
            Position = request.Position,
            Detail = request.Detail,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            Type = request.Type,
            CreatedAt = DateTime.UtcNow
        };

        await _teacherRepository.AddAsync(teacher, cancellationToken);
        await _teacherRepository.SaveChangesAsync(cancellationToken);

        var response = new TeacherResponse
        {
            Id = teacher.Id,
            CodeNumber = teacher.CodeNumber,
            Name = teacher.Name,
            Location = teacher.Location,
            Position = teacher.Position,
            Detail = teacher.Detail,
            Type = teacher.Type,
            CreatedAt = teacher.CreatedAt
        };

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] TeacherLoginRequest request,
        CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByCodeNumberAsync(request.CodeNumber, cancellationToken);

        if (teacher is null)
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        var isValid = _passwordHasher.VerifyPassword(request.Password, teacher.PasswordHash);

        if (!isValid)
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        var token = _jwtService.GenerateTeacherToken(teacher.Id, teacher.CodeNumber, teacher.Type);

        return Ok(new
        {
            token,
            teacherId = teacher.Id,
            codeNumber = teacher.CodeNumber,
            name = teacher.Name,
            type = teacher.Type
        });
    }

    [Authorize(Roles = "Teacher")]
    [HttpGet("secure")]
    public IActionResult Secure()
    {
        return Ok(new { message = "Teacher authorized." });
    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpPost("{id:int}/reset-password")]
    public async Task<IActionResult> ResetPassword(
        int id,
        [FromBody] ResetTeacherPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(id, cancellationToken);
        if (teacher is null)
        {
            return NotFound(new { message = "Teacher not found." });
        }

        teacher.PasswordHash = _passwordHasher.HashPassword(request.NewPassword);
        teacher.UpdatedAt = DateTime.UtcNow;

        await _teacherRepository.SaveChangesAsync(cancellationToken);

        return Ok(new { message = "Teacher password reset successful." });
    }
}