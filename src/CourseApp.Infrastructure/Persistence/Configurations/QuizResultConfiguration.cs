using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class QuizResultConfiguration : IEntityTypeConfiguration<QuizResult>
{
    public void Configure(EntityTypeBuilder<QuizResult> entity)
    {
        entity.ToTable("quiz_results");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id).UseIdentityAlwaysColumn();

        entity.Property(x => x.QuizId).HasColumnName("quiz_id").IsRequired();
        entity.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
        entity.Property(x => x.CourseId).HasColumnName("course_id").IsRequired();
        entity.Property(x => x.LessonNumber).HasColumnName("lesson_number").IsRequired();

        entity.Property(x => x.TotalQuestions).HasColumnName("total_questions").IsRequired();
        entity.Property(x => x.CorrectAnswers).HasColumnName("correct_answers").IsRequired();
        entity.Property(x => x.WrongAnswers).HasColumnName("wrong_answers").IsRequired();

        entity.Property(x => x.Score)
            .HasColumnName("score")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        entity.Property(x => x.IsPassed).HasColumnName("is_passed").IsRequired();
        entity.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
    }
}