using CourseApp.Application.DTOs.TeacherStatuses;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherStatusesController : ControllerBase
{
    private readonly ITeacherStatusRepository _teacherStatusRepository;

    public TeacherStatusesController(ITeacherStatusRepository teacherStatusRepository)
    {
        _teacherStatusRepository = teacherStatusRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var items = await _teacherStatusRepository.GetAllAsync(cancellationToken);
        return Ok(items.Select(MapToResponse));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var item = await _teacherStatusRepository.GetByIdAsync(id, cancellationToken);

        if (item is null)
        {
            return NotFound(new { message = "Teacher status not found." });
        }

        return Ok(MapToResponse(item));
    }

    [HttpGet("teacher/{teacherCode}")]
    public async Task<IActionResult> GetByTeacherCode(
        string teacherCode,
        CancellationToken cancellationToken)
    {
        var items = await _teacherStatusRepository.GetByTeacherCodeAsync(teacherCode, cancellationToken);
        return Ok(items.Select(MapToResponse));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTeacherStatusRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.TeacherCode))
        {
            return BadRequest(new { message = "TeacherCode is required." });
        }

        if (request.Courses is null || request.Courses.Count == 0)
        {
            return BadRequest(new { message = "At least one course is required." });
        }

        foreach (var course in request.Courses)
        {
            if (course.CourseId <= 0)
            {
                return BadRequest(new { message = "CourseId is required." });
            }

            if (string.IsNullOrWhiteSpace(course.CourseName))
            {
                return BadRequest(new { message = "CourseName is required." });
            }

            if (course.Lessons is null || course.Lessons.Count == 0)
            {
                return BadRequest(new { message = "At least one lesson is required." });
            }
        }

        var entity = new TeacherStatus
        {
            TeacherCode = request.TeacherCode,
            ImageUrl = request.ImageUrl,
            AdminApproved = false,
            CreatedAt = DateTime.UtcNow,
            Courses = request.Courses.Select(course => new TeacherStatusCourse
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                Lessons = course.Lessons.Select(lesson => new TeacherStatusLesson
                {
                    LessonNumber = lesson.LessonNumber,
                    VideoId = lesson.VideoId,
                    PdfId = lesson.PdfId
                }).ToList()
            }).ToList()
        };

        await _teacherStatusRepository.AddAsync(entity, cancellationToken);
        await _teacherStatusRepository.SaveChangesAsync(cancellationToken);

        return Ok(new
        {
            message = "Teacher status created successfully.",
            id = entity.Id
        });
    }

    [HttpPost("{id:int}/approve")]
    public async Task<IActionResult> Approve(
        int id,
        [FromBody] ApproveTeacherStatusRequest request,
        CancellationToken cancellationToken)
    {
        var entity = await _teacherStatusRepository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return NotFound(new { message = "Teacher status not found." });
        }

        entity.AdminApproved = request.AdminApproved;
        entity.UpdatedAt = DateTime.UtcNow;

        await _teacherStatusRepository.SaveChangesAsync(cancellationToken);

        return Ok(new
        {
            message = "Teacher status approval updated successfully.",
            id = entity.Id,
            entity.AdminApproved
        });
    }

    private static TeacherStatusResponse MapToResponse(TeacherStatus x)
    {
        return new TeacherStatusResponse
        {
            Id = x.Id,
            TeacherCode = x.TeacherCode,
            ImageUrl = x.ImageUrl,
            AdminApproved = x.AdminApproved,
            CreatedAt = x.CreatedAt,
            Courses = x.Courses.Select(c => new TeacherStatusCourseResponse
            {
                Id = c.Id,
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                Lessons = c.Lessons.Select(l => new TeacherStatusLessonResponse
                {
                    Id = l.Id,
                    LessonNumber = l.LessonNumber,
                    VideoId = l.VideoId,
                    PdfId = l.PdfId
                }).ToList()
            }).ToList()
        };
    }
}