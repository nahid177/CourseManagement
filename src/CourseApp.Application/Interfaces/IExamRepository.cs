using CourseApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.Interfaces
{
    public interface IExamRepository
    {
        Task AddAsync(Exam exam);
        Task SaveChangesAsync();

    }
}
