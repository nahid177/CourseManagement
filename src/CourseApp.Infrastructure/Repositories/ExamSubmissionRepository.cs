using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Application.Interfaces;
using CourseApp.Core.Entities;
using CourseApp.Infrastructure.Persistence;

namespace CourseApp.Infrastructure.Repositories;

public class ExamSubmissionRepository : IExamSubmissionRepository
{
    private readonly AppDbContext _db;

    public ExamSubmissionRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(ExamSubmission submission)
    {
        await _db.ExamSubmissions.AddAsync(submission);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}