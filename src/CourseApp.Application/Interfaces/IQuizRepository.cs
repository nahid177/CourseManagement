using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CourseApp.Core.Entities;

namespace CourseApp.Application.Interfaces;

public interface IQuizRepository
{
    Task<Quiz?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Quiz>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Quiz quiz, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}