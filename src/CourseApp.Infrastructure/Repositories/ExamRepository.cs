using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using CourseApp.Infrastructure.Persistence;

namespace CourseApp.Infrastructure.Repositories;

public class ExamRepository : IExamRepository
{
    private readonly AppDbContext _db;

    public ExamRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Exam exam)
    {
        await _db.Exams.AddAsync(exam);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}