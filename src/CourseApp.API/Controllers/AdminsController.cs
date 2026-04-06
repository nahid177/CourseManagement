using CourseApp.Application.DTOs.Admins;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminsController : ControllerBase
{
    private readonly IAdminRepository _adminRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public AdminsController(
        IAdminRepository adminRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _adminRepository = adminRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var admins = await _adminRepository.GetAllAsync(cancellationToken);

        var response = admins.Select(x => new AdminResponse
        {
            Id = x.Id,
            Code = x.Code,
            DriveNumber = x.DriveNumber,
            MustResetPassword = x.MustResetPassword,
            CreatedAt = x.CreatedAt
        });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateAdminRequest request,
        CancellationToken cancellationToken)
    {
        var existingAdmin = await _adminRepository.GetByCodeAsync(request.Code, cancellationToken);
        if (existingAdmin is not null)
        {
            return BadRequest(new { message = "Admin code already exists." });
        }

        var admin = new Admin
        {
            Code = request.Code,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            DriveNumber = request.DriveNumber,
            MustResetPassword = true,
            CreatedAt = DateTime.UtcNow
        };

        await _adminRepository.AddAsync(admin, cancellationToken);
        await _adminRepository.SaveChangesAsync(cancellationToken);

        var response = new AdminResponse
        {
            Id = admin.Id,
            Code = admin.Code,
            DriveNumber = admin.DriveNumber,
            MustResetPassword = admin.MustResetPassword,
            CreatedAt = admin.CreatedAt
        };

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var admin = await _adminRepository.GetByCodeAsync(request.Code, cancellationToken);

        if (admin is null)
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        var isValid = _passwordHasher.VerifyPassword(request.Password, admin.PasswordHash);

        if (!isValid)
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        var token = _jwtService.GenerateToken(admin.Id, admin.Code);

        return Ok(new
        {
            token,
            adminId = admin.Id,
            code = admin.Code,
            mustResetPassword = admin.MustResetPassword
        });
    }

    [HttpPost("{id:int}/reset-password")]
    public async Task<IActionResult> ResetPassword(
        int id,
        [FromBody] ResetAdminPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var admin = await _adminRepository.GetByIdAsync(id, cancellationToken);
        if (admin is null)
        {
            return NotFound(new { message = "Admin not found." });
        }

        admin.PasswordHash = _passwordHasher.HashPassword(request.NewPassword);
        admin.MustResetPassword = false;
        admin.UpdatedAt = DateTime.UtcNow;

        await _adminRepository.SaveChangesAsync(cancellationToken);

        return Ok(new { message = "Password reset successful." });
    }

    [Authorize]
    [HttpGet("secure")]
    public IActionResult Secure()
    {
        return Ok(new { message = "You are authorized." });
    }
}