using CourseApp.Application.DTOs.CourseCategories;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseCategoriesController : ControllerBase
{
    private readonly ICourseCategoryRepository _categoryRepository;

    public CourseCategoriesController(ICourseCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);

        var response = categories.Select(x => new CourseCategoryResponse
        {
            Id = x.Id,
            Name = x.Name,
            Slug = x.Slug,
            Description = x.Description,
            CreatedAt = x.CreatedAt
        });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCourseCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetBySlugAsync(request.Slug, cancellationToken);
        if (existingCategory is not null)
        {
            return BadRequest(new { message = "Category slug already exists." });
        }

        var category = new CourseCategory
        {
            Name = request.Name,
            Slug = request.Slug,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        await _categoryRepository.AddAsync(category, cancellationToken);
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return Ok(new CourseCategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
            CreatedAt = category.CreatedAt
        });
    }
}