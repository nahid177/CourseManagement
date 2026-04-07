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

public class TeacherRepository : ITeacherRepository
{
    private readonly AppDbContext _dbContext;

    public TeacherRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Teacher?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Teachers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Teacher?> GetByCodeNumberAsync(string codeNumber, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Teachers
            .FirstOrDefaultAsync(x => x.CodeNumber == codeNumber, cancellationToken);
    }

    public async Task<List<Teacher>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Teachers
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        await _dbContext.Teachers.AddAsync(teacher, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}