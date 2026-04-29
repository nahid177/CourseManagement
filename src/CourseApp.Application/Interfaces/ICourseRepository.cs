using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;

namespace CourseApp.Application.Interfaces;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Course?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<List<Course>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<Course>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default);
    Task AddAsync(Course course, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}