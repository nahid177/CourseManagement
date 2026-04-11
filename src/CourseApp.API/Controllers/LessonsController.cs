using CourseApp.Core.Entities;
using CourseApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly AppDbContext _db;

    public LessonsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Lesson lesson)
    {
        await _db.Lessons.AddAsync(lesson);
        await _db.SaveChangesAsync();

        return Ok(lesson);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _db.Lessons.ToListAsync();
        return Ok(data);
    }
}