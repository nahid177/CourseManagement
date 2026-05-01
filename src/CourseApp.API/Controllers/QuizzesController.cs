using CourseApp.Application.DTOs.Quizzes;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using CourseApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizzesController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public QuizzesController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var quizzes = await _dbContext.Quizzes
            .Include(x => x.Questions)
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);

        var response = quizzes.Select(MapToResponse);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var quiz = await _dbContext.Quizzes
            .Include(x => x.Questions)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (quiz is null)
        {
            return NotFound(new { message = "Quiz not found." });
        }

        return Ok(MapToResponse(quiz));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateQuizRequest request,
        CancellationToken cancellationToken)
    {
        if (request.TeacherVideoId <= 0)
        {
            return BadRequest(new { message = "TeacherVideoId is required." });
        }

        var video = await _dbContext.TeacherVideos
            .FirstOrDefaultAsync(x => x.Id == request.TeacherVideoId, cancellationToken);

        if (video is null)
        {
            return NotFound(new { message = "Teacher video/class not found." });
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest(new { message = "Quiz title is required." });
        }

        if (request.Questions is null || request.Questions.Count == 0)
        {
            return BadRequest(new { message = "At least one question is required." });
        }

        var normalizedQuestions = request.Questions.Select(x => new CreateQuizQuestionRequest
        {
            Question = x.Question,
            OptionA = x.OptionA,
            OptionB = x.OptionB,
            OptionC = x.OptionC,
            OptionD = x.OptionD,
            CorrectAnswer = x.CorrectAnswer.Trim().ToUpper()
        }).ToList();

        var invalidAnswer = normalizedQuestions.Any(x =>
            x.CorrectAnswer != "A" &&
            x.CorrectAnswer != "B" &&
            x.CorrectAnswer != "C" &&
            x.CorrectAnswer != "D");

        if (invalidAnswer)
        {
            return BadRequest(new { message = "Correct answer must be A, B, C, or D." });
        }

        var quiz = new Quiz
        {
            CourseId = video.CourseId,
            LessonId = video.LessonId,
            TeacherVideoId = video.Id,
            TeacherId = video.TeacherId,
            AdminId = request.AdminId,
            Title = request.Title,
            CreatedAt = DateTime.UtcNow,
            Questions = normalizedQuestions.Select(q => new QuizQuestion
            {
                Question = q.Question,
                OptionA = q.OptionA,
                OptionB = q.OptionB,
                OptionC = q.OptionC,
                OptionD = q.OptionD,
                CorrectAnswer = q.CorrectAnswer
            }).ToList()
        };

        await _dbContext.Quizzes.AddAsync(quiz, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(new
        {
            message = "Quiz created successfully.",
            quizId = quiz.Id,
            teacherVideoId = quiz.TeacherVideoId
        });
    }

    private static QuizResponse MapToResponse(Quiz quiz)
    {
        return new QuizResponse
        {
            Id = quiz.Id,
            CourseId = quiz.CourseId,
            LessonId = quiz.LessonId,
            TeacherVideoId = quiz.TeacherVideoId,
            TeacherId = quiz.TeacherId,
            AdminId = quiz.AdminId,
            Title = quiz.Title,
            CreatedAt = quiz.CreatedAt,
            Questions = quiz.Questions.Select(q => new QuizQuestionResponse
            {
                Id = q.Id,
                Question = q.Question,
                OptionA = q.OptionA,
                OptionB = q.OptionB,
                OptionC = q.OptionC,
                OptionD = q.OptionD,
                CorrectAnswer = q.CorrectAnswer
            }).ToList()
        };
    }
}