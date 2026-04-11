using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;

namespace CourseApp.Application.Interfaces;

public interface IStudentEnrollmentRepository
{
    Task<StudentEnrollment?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<StudentEnrollment>> GetByStudentIdAsync(int studentId, CancellationToken cancellationToken = default);
    Task AddAsync(StudentEnrollment enrollment, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}