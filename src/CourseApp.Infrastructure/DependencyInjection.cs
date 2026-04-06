using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CourseApp.Application.Interfaces;
using CourseApp.Infrastructure.Persistence;
using CourseApp.Infrastructure.Repositories;
using CourseApp.Infrastructure.Security;

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
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}