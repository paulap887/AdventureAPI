using Adventure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Data
{
    /// <summary>
    /// AdventureDbContext
    /// </summary>
    public class AdventureDbContext : DbContext
    {
        public DbSet<Adventure.Core.Models.Adventure> Adventure { get; set; }
        public DbSet<Adventure.Core.Models.UserAdventure> UserAdventure { get; set; }
        public DbSet<Adventure.Core.Models.UserAdventureQuestion> UserAdventureQuestion { get; set; }
        public AdventureDbContext(DbContextOptions<AdventureDbContext> options)
                : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AdventureConfiguration());
            builder.ApplyConfiguration(new QuestionConfiguration());
            builder.ApplyConfiguration(new UserAdventureConfiguration());
            builder.ApplyConfiguration(new UserAdventureQuestionConfiguration());
        }
    }
}
