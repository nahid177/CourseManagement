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

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _dbContext;

    public QuizRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Quiz?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Quizzes
            .Include(x => x.Questions)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Quiz>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Quizzes
            .Include(x => x.Questions)
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Quiz quiz, CancellationToken cancellationToken = default)
    {
        await _dbContext.Quizzes.AddAsync(quiz, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}