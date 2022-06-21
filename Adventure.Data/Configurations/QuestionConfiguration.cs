using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Data.Configurations
{
    /// <summary>
    /// QuestionConfiguration
    /// </summary>
    public class QuestionConfiguration : IEntityTypeConfiguration<Adventure.Core.Models.Question>
    {
        public void Configure(EntityTypeBuilder<Adventure.Core.Models.Question> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id);

            builder
                .Property(m => m.QuestionDescription) 
                .IsRequired()
                .HasMaxLength(100); 

            builder
              .HasOne(c => c.Adventure)
              .WithMany(c => c.Questions)
              .HasForeignKey(c => c.AdventureId) 
              .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(c => c.ParentQuestion)
               .WithMany(c => c.Questions)
               .HasForeignKey(c => c.ParentQuestionId) 
               .OnDelete(DeleteBehavior.Restrict);
             
            builder
                .ToTable("Question");
        }
    }
}
