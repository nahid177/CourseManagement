using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;

namespace CourseApp.Application.Interfaces;

public interface IAdminRepository
{
    Task<Admin?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Admin?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Admin admin, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}