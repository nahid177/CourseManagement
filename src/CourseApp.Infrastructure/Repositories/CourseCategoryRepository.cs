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

public class CourseCategoryRepository : ICourseCategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CourseCategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CourseCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CourseCategories
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<CourseCategory?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CourseCategories
            .FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<List<CourseCategory>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.CourseCategories
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CourseCategory category, CancellationToken cancellationToken = default)
    {
        await _dbContext.CourseCategories.AddAsync(category, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}