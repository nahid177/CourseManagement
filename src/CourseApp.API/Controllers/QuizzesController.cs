using CourseApp.Application.DTOs.Quizzes;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizzesController : ControllerBase
{
    private readonly IQuizRepository _quizRepository;

    public QuizzesController(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var quizzes = await _quizRepository.GetAllAsync(cancellationToken);

        var response = quizzes.Select(x => new QuizResponse
        {
            Id = x.Id,
            CourseId = x.CourseId,
            LessonNumber = x.LessonNumber,
            VideoId = x.VideoId,
            TeacherId = x.TeacherId,
            AdminId = x.AdminId,
            Title = x.Title,
            CreatedAt = x.CreatedAt,
            Questions = x.Questions.Select(q => new QuizQuestionResponse
            {
                Id = q.Id,
                Question = q.Question,
                OptionA = q.OptionA,
                OptionB = q.OptionB,
                OptionC = q.OptionC,
                OptionD = q.OptionD,
                CorrectAnswer = q.CorrectAnswer
            }).ToList()
        });

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepository.GetByIdAsync(id, cancellationToken);
        if (quiz is null)
        {
            return NotFound(new { message = "Quiz not found." });
        }

        var response = new QuizResponse
        {
            Id = quiz.Id,
            CourseId = quiz.CourseId,
            LessonNumber = quiz.LessonNumber,
            VideoId = quiz.VideoId,
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

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateQuizRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Questions is null || request.Questions.Count == 0)
        {
            return BadRequest(new { message = "At least one question is required." });
        }

        var invalidAnswer = request.Questions.Any(x =>
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
            CourseId = request.CourseId,
            LessonNumber = request.LessonNumber,
            VideoId = request.VideoId,
            TeacherId = request.TeacherId,
            AdminId = request.AdminId,
            Title = request.Title,
            CreatedAt = DateTime.UtcNow,
            Questions = request.Questions.Select(q => new QuizQuestion
            {
                Question = q.Question,
                OptionA = q.OptionA,
                OptionB = q.OptionB,
                OptionC = q.OptionC,
                OptionD = q.OptionD,
                CorrectAnswer = q.CorrectAnswer
            }).ToList()
        };

        await _quizRepository.AddAsync(quiz, cancellationToken);
        await _quizRepository.SaveChangesAsync(cancellationToken);

        return Ok(new
        {
            message = "Quiz created successfully.",
            quizId = quiz.Id
        });
    }
}