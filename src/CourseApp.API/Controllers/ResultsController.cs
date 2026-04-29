using CourseApp.Application.DTOs.Results;
using CourseApp.Core.Entities;
using CourseApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResultsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ResultsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("quiz")]
    public async Task<IActionResult> CreateQuizResult(
        [FromBody] CreateQuizResultRequest request,
        CancellationToken cancellationToken)
    {
        if (request.TotalQuestions <= 0)
        {
            return BadRequest(new { message = "Total questions must be greater than 0." });
        }

        if (request.CorrectAnswers < 0 || request.CorrectAnswers > request.TotalQuestions)
        {
            return BadRequest(new { message = "Correct answers value is invalid." });
        }

        var wrongAnswers = request.TotalQuestions - request.CorrectAnswers;
        var score = ((decimal)request.CorrectAnswers / request.TotalQuestions) * 100;
        var isPassed = score >= 40;

        var result = new QuizResult
        {
            QuizId = request.QuizId,
            UserId = request.UserId,
            CourseId = request.CourseId,
            LessonNumber = request.LessonNumber,
            TotalQuestions = request.TotalQuestions,
            CorrectAnswers = request.CorrectAnswers,
            WrongAnswers = wrongAnswers,
            Score = score,
            IsPassed = isPassed,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.QuizResults.AddAsync(result, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(new
        {
            message = "Quiz result created successfully.",
            result.Id,
            result.Score,
            result.IsPassed
        });
    }

    [HttpPost("exam")]
    public async Task<IActionResult> CreateExamResult(
        [FromBody] CreateExamResultRequest request,
        CancellationToken cancellationToken)
    {
        if (request.TotalMarks <= 0)
        {
            return BadRequest(new { message = "Total marks must be greater than 0." });
        }

        if (request.Marks < 0 || request.Marks > request.TotalMarks)
        {
            return BadRequest(new { message = "Marks value is invalid." });
        }

        var percentage = (request.Marks / request.TotalMarks) * 100;
        var isPassed = percentage >= 40;

        var result = new ExamResult
        {
            ExamId = request.ExamId,
            UserId = request.UserId,
            TeacherId = request.TeacherId,
            CourseId = request.CourseId,
            LessonId = request.LessonId,
            Marks = request.Marks,
            TotalMarks = request.TotalMarks,
            IsPassed = isPassed,
            TeacherFeedback = request.TeacherFeedback,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.ExamResults.AddAsync(result, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(new
        {
            message = "Exam result created successfully.",
            result.Id,
            percentage,
            result.IsPassed
        });
    }

    [HttpGet("quiz/user/{userId:int}")]
    public async Task<IActionResult> GetQuizResultsByUser(int userId, CancellationToken cancellationToken)
    {
        var results = await _dbContext.QuizResults
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);

        return Ok(results);
    }

    [HttpGet("exam/user/{userId:int}")]
    public async Task<IActionResult> GetExamResultsByUser(int userId, CancellationToken cancellationToken)
    {
        var results = await _dbContext.ExamResults
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);

        return Ok(results);
    }
}