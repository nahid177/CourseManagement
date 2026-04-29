using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;

namespace CourseApp.Application.Interfaces;

public interface ICourseCategoryRepository
{
    Task<CourseCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CourseCategory?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<List<CourseCategory>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(CourseCategory category, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}