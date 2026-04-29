using CourseApp.Application.DTOs.Courses;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseCategoryRepository _categoryRepository;

    public CoursesController(
        ICourseRepository courseRepository,
        ICourseCategoryRepository categoryRepository)
    {
        _courseRepository = courseRepository;
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAllAsync(cancellationToken);

        var response = courses.Select(MapToResponse);

        return Ok(response);
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<IActionResult> GetByCategory(int categoryId, CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetByCategoryIdAsync(categoryId, cancellationToken);

        var response = courses.Select(MapToResponse);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCourseRequest request,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CourseCategoryId, cancellationToken);
        if (category is null)
        {
            return NotFound(new { message = "Course category not found." });
        }

        var existingCourse = await _courseRepository.GetBySlugAsync(request.Slug, cancellationToken);
        if (existingCourse is not null)
        {
            return BadRequest(new { message = "Course slug already exists." });
        }

        var course = new Course
        {
            CourseCategoryId = request.CourseCategoryId,
            TeacherId = request.TeacherId,
            Title = request.Title,
            Slug = request.Slug,
            Description = request.Description,
            ThumbnailUrl = request.ThumbnailUrl,
            Price = request.Price,
            DurationHours = request.DurationHours,
            Level = request.Level,
            IsPublished = request.IsPublished,
            CreatedAt = DateTime.UtcNow
        };

        await _courseRepository.AddAsync(course, cancellationToken);
        await _courseRepository.SaveChangesAsync(cancellationToken);

        course.CourseCategory = category;

        return Ok(MapToResponse(course));
    }

    private static CourseResponse MapToResponse(Course course)
    {
        return new CourseResponse
        {
            Id = course.Id,
            CourseCategoryId = course.CourseCategoryId,
            CategoryName = course.CourseCategory.Name,
            TeacherId = course.TeacherId,
            Title = course.Title,
            Slug = course.Slug,
            Description = course.Description,
            ThumbnailUrl = course.ThumbnailUrl,
            Price = course.Price,
            DurationHours = course.DurationHours,
            Level = course.Level,
            IsPublished = course.IsPublished,
            CreatedAt = course.CreatedAt
        };
    }
}