using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseApp.Infrastructure.Persistence.Configurations;

public class ExamSubmissionConfiguration : IEntityTypeConfiguration<ExamSubmission>
{
    public void Configure(EntityTypeBuilder<ExamSubmission> entity)
    {
        entity.ToTable("exam_submissions");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        entity.Property(x => x.ExamId)
            .HasColumnName("exam_id");

        entity.Property(x => x.UserId)
            .HasColumnName("user_id");

        entity.Property(x => x.ExamAnsUrl)
            .HasColumnName("exam_ans_url")
            .HasMaxLength(500);

        entity.Property(x => x.AnswerDetail)
            .HasColumnName("answer_detail")
            .HasColumnType("text");

        entity.Property(x => x.SubmittedAt)
            .HasColumnName("submitted_at");
    }
}
