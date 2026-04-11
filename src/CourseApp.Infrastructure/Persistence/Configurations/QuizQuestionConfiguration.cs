using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
{
    public void Configure(EntityTypeBuilder<QuizQuestion> entity)
    {
        entity.ToTable("quiz_questions");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        entity.Property(x => x.QuizId)
            .HasColumnName("quiz_id")
            .IsRequired();

        entity.Property(x => x.Question)
            .HasColumnName("question")
            .HasColumnType("text")
            .IsRequired();

        entity.Property(x => x.OptionA)
            .HasColumnName("option_a")
            .HasColumnType("text")
            .IsRequired();

        entity.Property(x => x.OptionB)
            .HasColumnName("option_b")
            .HasColumnType("text")
            .IsRequired();

        entity.Property(x => x.OptionC)
            .HasColumnName("option_c")
            .HasColumnType("text")
            .IsRequired();

        entity.Property(x => x.OptionD)
            .HasColumnName("option_d")
            .HasColumnType("text")
            .IsRequired();

        entity.Property(x => x.CorrectAnswer)
            .HasColumnName("correct_answer")
            .HasMaxLength(1)
            .IsRequired();
    }
}