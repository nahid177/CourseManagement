using CourseApp.Application.DTOs.Exams;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExamsController : ControllerBase
{
    private readonly IExamRepository _examRepo;
    private readonly IExamSubmissionRepository _submissionRepo;

    public ExamsController(
        IExamRepository examRepo,
        IExamSubmissionRepository submissionRepo)
    {
        _examRepo = examRepo;
        _submissionRepo = submissionRepo;
    }

    [HttpPost]
    public async Task<IActionResult> CreateExam(CreateExamRequest request)
    {
        var exam = new Exam
        {
            CourseId = request.CourseId,
            LessonId = request.LessonId,
            UserId = request.UserId,
            TeacherId = request.TeacherId,
            ExamUrl = request.ExamUrl,
            ExamDetail = request.ExamDetail
        };

        await _examRepo.AddAsync(exam);
        await _examRepo.SaveChangesAsync();

        return Ok(new { exam.Id });
    }

    [HttpPost("submit")]
    public async Task<IActionResult> Submit(CreateExamSubmissionRequest request)
    {
        var submission = new ExamSubmission
        {
            ExamId = request.ExamId,
            UserId = request.UserId,
            ExamAnsUrl = request.ExamAnsUrl,
            AnswerDetail = request.AnswerDetail
        };

        await _submissionRepo.AddAsync(submission);
        await _submissionRepo.SaveChangesAsync();

        return Ok("Submitted");
    }
}