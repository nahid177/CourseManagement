using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CourseApp.Core.Entities;

namespace CourseApp.Application.Interfaces;

public interface ITeacherStatusRepository
{
    Task<TeacherStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<TeacherStatus>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<TeacherStatus>> GetByTeacherCodeAsync(string teacherCode, CancellationToken cancellationToken = default);
    Task AddAsync(TeacherStatus teacherStatus, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}