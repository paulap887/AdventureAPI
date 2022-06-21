using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Data.Configurations
{
    /// <summary>
    /// UserAdventureQuestionConfiguration
    /// </summary>
    public class UserAdventureQuestionConfiguration : IEntityTypeConfiguration<Adventure.Core.Models.UserAdventureQuestion>
    {
        public void Configure(EntityTypeBuilder<Adventure.Core.Models.UserAdventureQuestion> builder)
        {
            builder
              .HasKey(m => m.Id);

            builder
                .Property(m => m.Id);

            builder
               .HasOne(c => c.UserAdventure)
               .WithMany(c => c.UserAdventureQuestions)
               .HasForeignKey(c => c.UserAdventureId)
               .OnDelete(DeleteBehavior.Restrict);
             

            builder
                .HasOne(c => c.Question)
                .WithMany(c => c.UserAdventureQuestions)
                .HasForeignKey(c => c.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                    .ToTable("UserAdventureQuestion");

        }
    }
}
