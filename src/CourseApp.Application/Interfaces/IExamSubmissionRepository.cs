using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;

namespace CourseApp.Application.Interfaces;

public interface IExamSubmissionRepository
{
    Task AddAsync(ExamSubmission submission);
    Task SaveChangesAsync();
}