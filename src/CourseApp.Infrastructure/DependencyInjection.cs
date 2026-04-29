using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Application.Interfaces;
using CourseApp.Infrastructure.Persistence;
using CourseApp.Infrastructure.Repositories;
using CourseApp.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IPasswordHasher, AppPasswordHasher>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ITeacherStatusRepository, TeacherStatusRepository>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentEnrollmentRepository, StudentEnrollmentRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IExamRepository, ExamRepository>();
        services.AddScoped<IExamSubmissionRepository, ExamSubmissionRepository>();

        return services;
    }
}