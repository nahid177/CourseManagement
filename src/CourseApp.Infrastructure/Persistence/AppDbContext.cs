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
    public DbSet<Lesson> Lessons => Set<Lesson>();

    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentEnrollment> StudentEnrollments => Set<StudentEnrollment>();

    public DbSet<Payment> Payments => Set<Payment>();

    public DbSet<Quiz> Quizzes => Set<Quiz>();
    public DbSet<QuizQuestion> QuizQuestions => Set<QuizQuestion>();

    public DbSet<Exam> Exams => Set<Exam>();

    public DbSet<ExamSubmission> ExamSubmissions => Set<ExamSubmission>();
    public DbSet<QuizResult> QuizResults => Set<QuizResult>();
    public DbSet<ExamResult> ExamResults => Set<ExamResult>();

    public DbSet<CourseCategory> CourseCategories => Set<CourseCategory>();
    public DbSet<Course> Courses => Set<Course>();

    public DbSet<BookCategory> BookCategories => Set<BookCategory>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookCategoryMapping> BookCategoryMappings => Set<BookCategoryMapping>();
    public DbSet<BookOrder> BookOrders => Set<BookOrder>();
    public DbSet<BookOrderItem> BookOrderItems => Set<BookOrderItem>();
    public DbSet<BookPayment> BookPayments => Set<BookPayment>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}