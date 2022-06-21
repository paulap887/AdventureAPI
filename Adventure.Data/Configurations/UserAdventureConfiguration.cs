using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Data.Configurations
{
    /// <summary>
    /// UserAdventureConfiguration
    /// </summary>
    public class UserAdventureConfiguration : IEntityTypeConfiguration<Adventure.Core.Models.UserAdventure>
    {
        public void Configure(EntityTypeBuilder<Adventure.Core.Models.UserAdventure> builder) 
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id);

            builder
             .HasOne(c => c.Adventure)
             .WithMany(c => c.UserAdventures)
             .HasForeignKey(c => c.AdventureId)
             .OnDelete(DeleteBehavior.Restrict);

            builder
                    .ToTable("UserAdventure");
        }
    }
}
