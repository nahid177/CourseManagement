using Microsoft.AspNetCore.Mvc;
using CourseApp.Application.DTOs.Admins;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;

namespace CourseApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminsController : ControllerBase
{
    private readonly IAdminRepository _adminRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AdminsController(
        IAdminRepository adminRepository,
        IPasswordHasher passwordHasher)
    {
        _adminRepository = adminRepository;
        _passwordHasher = passwordHasher;
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
}