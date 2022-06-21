using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Data.Configurations
{
    /// <summary>
    /// AdventureConfiguration
    /// </summary>
    public class AdventureConfiguration : IEntityTypeConfiguration<Adventure.Core.Models.Adventure>
    {
        public void Configure(EntityTypeBuilder<Adventure.Core.Models.Adventure> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id);

            builder
                .Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder
                   .ToTable("Adventure");
        }
    }
}
