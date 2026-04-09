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

public class TeacherStatusRepository : ITeacherStatusRepository
{
    private readonly AppDbContext _dbContext;

    public TeacherStatusRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TeacherStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.TeacherStatuses
            .Include(x => x.Courses)
                .ThenInclude(x => x.Lessons)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<TeacherStatus>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.TeacherStatuses
            .Include(x => x.Courses)
                .ThenInclude(x => x.Lessons)
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TeacherStatus>> GetByTeacherCodeAsync(string teacherCode, CancellationToken cancellationToken = default)
    {
        return await _dbContext.TeacherStatuses
            .Include(x => x.Courses)
                .ThenInclude(x => x.Lessons)
            .AsNoTracking()
            .Where(x => x.TeacherCode == teacherCode)
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TeacherStatus teacherStatus, CancellationToken cancellationToken = default)
    {
        await _dbContext.TeacherStatuses.AddAsync(teacherStatus, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}