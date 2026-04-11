using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using CourseApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CourseApp.Infrastructure.Repositories;

public class StudentEnrollmentRepository : IStudentEnrollmentRepository
{
    private readonly AppDbContext _dbContext;

    public StudentEnrollmentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StudentEnrollment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.StudentEnrollments
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<StudentEnrollment>> GetByStudentIdAsync(int studentId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.StudentEnrollments
            .AsNoTracking()
            .Where(x => x.StudentId == studentId)
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(StudentEnrollment enrollment, CancellationToken cancellationToken = default)
    {
        await _dbContext.StudentEnrollments.AddAsync(enrollment, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}