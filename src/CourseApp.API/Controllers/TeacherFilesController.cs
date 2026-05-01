using CourseApp.Core.Entities;
using CourseApp.Infrastructure.Persistence;
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

    [HttpPost("upload-video")]
    [RequestSizeLimit(500_000_000)]
    public async Task<IActionResult> UploadVideo(
        [FromForm] int courseId,
        [FromForm] int teacherId,
        [FromForm] int lessonId,
        [FromForm] string className,
        [FromForm] string? classDetail,
        [FromForm] string videoName,
        [FromForm] IFormFile video,
        CancellationToken cancellationToken)
    {
        if (courseId <= 0)
            return BadRequest(new { message = "CourseId is required." });

        if (teacherId <= 0)
            return BadRequest(new { message = "TeacherId is required." });

        if (lessonId <= 0)
            return BadRequest(new { message = "LessonId is required." });

        if (string.IsNullOrWhiteSpace(className))
            return BadRequest(new { message = "Class name is required." });

        if (string.IsNullOrWhiteSpace(videoName))
            return BadRequest(new { message = "Video name is required." });

        if (video is null || video.Length == 0)
            return BadRequest(new { message = "Video file is required." });

        var allowedExtensions = new[] { ".mp4", ".mov", ".avi", ".mkv", ".webm" };
        var extension = Path.GetExtension(video.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
        {
            return BadRequest(new
            {
                message = "Invalid video file type. Allowed: mp4, mov, avi, mkv, webm."
            });
        }

        var wwwroot = _environment.WebRootPath;

        if (string.IsNullOrWhiteSpace(wwwroot))
        {
            wwwroot = Path.Combine(_environment.ContentRootPath, "wwwroot");
            Directory.CreateDirectory(wwwroot);
        }

        var uploadsRoot = Path.Combine(wwwroot, "uploads", "videos");
        Directory.CreateDirectory(uploadsRoot);

        var uniqueFileName = $"{Guid.NewGuid()}{extension}";
        var physicalPath = Path.Combine(uploadsRoot, uniqueFileName);

        await using (var stream = new FileStream(physicalPath, FileMode.Create))
        {
            await video.CopyToAsync(stream, cancellationToken);
        }

        var entity = new TeacherVideo
        {
            CourseId = courseId,
            TeacherId = teacherId,
            LessonId = lessonId,
            ClassName = className,
            ClassDetail = classDetail,
            VideoName = videoName,
            OriginalFileName = video.FileName,
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
            entity.CourseId,
            entity.TeacherId,
            entity.LessonId,
            entity.ClassName,
            entity.ClassDetail,
            entity.VideoName,
            entity.OriginalFileName,
            entity.VideoFilePath,
            entity.ContentType,
            entity.FileSize,
            entity.CreatedAt
        });
    }

    [HttpPost("upload-document")]
    [RequestSizeLimit(100_000_000)]
    public async Task<IActionResult> UploadDocument(
        [FromForm] int teacherId,
        [FromForm] int lessonId,
        [FromForm] IFormFile pdf,
        [FromForm] IFormFile? image,
        CancellationToken cancellationToken)
    {
        if (teacherId <= 0)
            return BadRequest(new { message = "TeacherId is required." });

        if (lessonId <= 0)
            return BadRequest(new { message = "LessonId is required." });

        if (pdf is null || pdf.Length == 0)
            return BadRequest(new { message = "PDF file is required." });

        var pdfExtension = Path.GetExtension(pdf.FileName).ToLowerInvariant();

        if (pdfExtension != ".pdf")
            return BadRequest(new { message = "Only PDF files are allowed." });

        var wwwroot = _environment.WebRootPath;

        if (string.IsNullOrWhiteSpace(wwwroot))
        {
            wwwroot = Path.Combine(_environment.ContentRootPath, "wwwroot");
            Directory.CreateDirectory(wwwroot);
        }

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
                return BadRequest(new { message = "Invalid image file type." });

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
            entity.ImageFilePath,
            entity.ContentType,
            entity.FileSize,
            entity.CreatedAt
        });
    }

    [HttpGet("videos")]
    public async Task<IActionResult> GetVideos(CancellationToken cancellationToken)
    {
        var data = await _dbContext.TeacherVideos
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);

        return Ok(data);
    }

    [HttpGet("videos/{id:int}")]
    public async Task<IActionResult> GetVideoById(int id, CancellationToken cancellationToken)
    {
        var data = await _dbContext.TeacherVideos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (data is null)
            return NotFound(new { message = "Video not found." });

        return Ok(data);
    }

    [HttpGet("videos/lesson/{lessonId:int}")]
    public async Task<IActionResult> GetVideosByLesson(int lessonId, CancellationToken cancellationToken)
    {
        var data = await _dbContext.TeacherVideos
            .AsNoTracking()
            .Where(x => x.LessonId == lessonId)
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);

        return Ok(data);
    }

    [HttpGet("videos/course/{courseId:int}")]
    public async Task<IActionResult> GetVideosByCourse(int courseId, CancellationToken cancellationToken)
    {
        var data = await _dbContext.TeacherVideos
            .AsNoTracking()
            .Where(x => x.CourseId == courseId)
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);

        return Ok(data);
    }

    [HttpGet("videos/teacher/{teacherId:int}")]
    public async Task<IActionResult> GetVideosByTeacher(int teacherId, CancellationToken cancellationToken)
    {
        var data = await _dbContext.TeacherVideos
            .AsNoTracking()
            .Where(x => x.TeacherId == teacherId)
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);

        return Ok(data);
    }

    [HttpGet("documents")]
    public async Task<IActionResult> GetDocuments(CancellationToken cancellationToken)
    {
        var data = await _dbContext.TeacherDocuments
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);

        return Ok(data);
    }

    [HttpGet("documents/lesson/{lessonId:int}")]
    public async Task<IActionResult> GetDocumentsByLesson(int lessonId, CancellationToken cancellationToken)
    {
        var data = await _dbContext.TeacherDocuments
            .AsNoTracking()
            .Where(x => x.LessonId == lessonId)
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);

        return Ok(data);
    }
}