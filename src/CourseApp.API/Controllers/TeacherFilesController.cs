using CourseApp.Core.Entities;
using CourseApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherFilesController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IWebHostEnvironment _environment;

    public TeacherFilesController(AppDbContext dbContext, IWebHostEnvironment environment)
    {
        _dbContext = dbContext;
        _environment = environment;
    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpPost("upload-video")]
    [RequestSizeLimit(500_000_000)] // 500 MB
    public async Task<IActionResult> UploadVideo(
        [FromForm] int teacherId,
        [FromForm] int lessonId,
        [FromForm] IFormFile video,
        CancellationToken cancellationToken)
    {
        if (video is null || video.Length == 0)
        {
            return BadRequest(new { message = "Video file is required." });
        }

        var allowedExtensions = new[] { ".mp4", ".mov", ".avi", ".mkv", ".webm" };
        var extension = Path.GetExtension(video.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
        {
            return BadRequest(new { message = "Invalid video file type." });
        }

        var uploadsRoot = Path.Combine(_environment.WebRootPath ?? CreateWwwRoot(), "uploads", "videos");
        Directory.CreateDirectory(uploadsRoot);

        var uniqueFileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsRoot, uniqueFileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await video.CopyToAsync(stream, cancellationToken);
        }

        var entity = new TeacherVideo
        {
            TeacherId = teacherId,
            LessonId = lessonId,
            VideoName = video.FileName,
            VideoFilePath = $"/uploads/videos/{uniqueFileName}",
            ContentType = video.ContentType,
            FileSize = video.Length,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.TeacherVideos.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(new
        {
            message = "Video uploaded successfully.",
            entity.Id,
            entity.TeacherId,
            entity.LessonId,
            entity.VideoName,
            entity.VideoFilePath
        });
    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpPost("upload-document")]
    [RequestSizeLimit(100_000_000)] // 100 MB
    public async Task<IActionResult> UploadDocument(
        [FromForm] int teacherId,
        [FromForm] int lessonId,
        [FromForm] IFormFile pdf,
        [FromForm] IFormFile? image,
        CancellationToken cancellationToken)
    {
        if (pdf is null || pdf.Length == 0)
        {
            return BadRequest(new { message = "PDF file is required." });
        }

        var pdfExtension = Path.GetExtension(pdf.FileName).ToLowerInvariant();
        if (pdfExtension != ".pdf")
        {
            return BadRequest(new { message = "Only PDF files are allowed." });
        }

        var wwwroot = _environment.WebRootPath ?? CreateWwwRoot();

        var pdfRoot = Path.Combine(wwwroot, "uploads", "pdfs");
        var imageRoot = Path.Combine(wwwroot, "uploads", "images");

        Directory.CreateDirectory(pdfRoot);
        Directory.CreateDirectory(imageRoot);

        var pdfUniqueFileName = $"{Guid.NewGuid()}{pdfExtension}";
        var pdfPhysicalPath = Path.Combine(pdfRoot, pdfUniqueFileName);

        await using (var pdfStream = new FileStream(pdfPhysicalPath, FileMode.Create))
        {
            await pdf.CopyToAsync(pdfStream, cancellationToken);
        }

        string? imageFileName = null;
        string? imageVirtualPath = null;

        if (image is not null && image.Length > 0)
        {
            var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var imageExtension = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (!allowedImageExtensions.Contains(imageExtension))
            {
                return BadRequest(new { message = "Invalid image file type." });
            }

            var imageUniqueFileName = $"{Guid.NewGuid()}{imageExtension}";
            var imagePhysicalPath = Path.Combine(imageRoot, imageUniqueFileName);

            await using (var imageStream = new FileStream(imagePhysicalPath, FileMode.Create))
            {
                await image.CopyToAsync(imageStream, cancellationToken);
            }

            imageFileName = image.FileName;
            imageVirtualPath = $"/uploads/images/{imageUniqueFileName}";
        }

        var entity = new TeacherDocument
        {
            TeacherId = teacherId,
            LessonId = lessonId,
            PdfFileName = pdf.FileName,
            PdfFilePath = $"/uploads/pdfs/{pdfUniqueFileName}",
            ImageFileName = imageFileName,
            ImageFilePath = imageVirtualPath,
            ContentType = pdf.ContentType,
            FileSize = pdf.Length,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.TeacherDocuments.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(new
        {
            message = "Document uploaded successfully.",
            entity.Id,
            entity.TeacherId,
            entity.LessonId,
            entity.PdfFileName,
            entity.PdfFilePath,
            entity.ImageFileName,
            entity.ImageFilePath
        });
    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpGet("videos")]
    public async Task<IActionResult> GetVideos(CancellationToken cancellationToken)
    {
        var data = await _dbContext.TeacherVideos
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);

        return Ok(data);
    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpGet("documents")]
    public async Task<IActionResult> GetDocuments(CancellationToken cancellationToken)
    {
        var data = await _dbContext.TeacherDocuments
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);

        return Ok(data);
    }

    private string CreateWwwRoot()
    {
        var path = Path.Combine(_environment.ContentRootPath, "wwwroot");
        Directory.CreateDirectory(path);
        return path;
    }
}