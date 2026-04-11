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

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _dbContext;

    public StudentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Students
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Student?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Students
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<Student?> GetByNumberAsync(string number, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Students
            .FirstOrDefaultAsync(x => x.Number == number, cancellationToken);
    }

    public async Task<Student?> GetByResetCodeAsync(string resetCode, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Students
            .FirstOrDefaultAsync(x => x.ResetCode == resetCode, cancellationToken);
    }

    public async Task<List<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Students
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Student student, CancellationToken cancellationToken = default)
    {
        await _dbContext.Students.AddAsync(student, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}