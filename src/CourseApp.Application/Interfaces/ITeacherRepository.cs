using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;

namespace CourseApp.Application.Interfaces;

public interface ITeacherRepository
{
    Task<Teacher?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Teacher?> GetByCodeNumberAsync(string codeNumber, CancellationToken cancellationToken = default);
    Task<List<Teacher>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Teacher teacher, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}