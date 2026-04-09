using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<Teacher> Teachers => Set<Teacher>();

    public DbSet<TeacherStatus> TeacherStatuses => Set<TeacherStatus>();
    public DbSet<TeacherStatusCourse> TeacherStatusCourses => Set<TeacherStatusCourse>();
    public DbSet<TeacherStatusLesson> TeacherStatusLessons => Set<TeacherStatusLesson>();

    public DbSet<TeacherVideo> TeacherVideos => Set<TeacherVideo>();
    public DbSet<TeacherDocument> TeacherDocuments => Set<TeacherDocument>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}