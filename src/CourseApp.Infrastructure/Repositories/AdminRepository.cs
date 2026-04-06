using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using CourseApp.Infrastructure.Persistence;

namespace CourseApp.Infrastructure.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _dbContext;

    public AdminRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Admin?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Admins
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Admin?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Admins
            .FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
    }

    public async Task<List<Admin>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Admins
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Admin admin, CancellationToken cancellationToken = default)
    {
        await _dbContext.Admins.AddAsync(admin, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}