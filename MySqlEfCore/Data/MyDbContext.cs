using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MySqlEfCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Data
{
    public class MyDbContext : DbContext
    {
        // Database tables for the Quiz game
        public DbSet<Category> Categories { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizCategoryLength> QuizCategoryLengths { get; set; }        
        public DbSet<QuizGame> QuizGames { get; set; }
        public DbSet<QuizGameQuestion> QuizGameQuestions { get; set; }

        // Database table for the Leaderboard
        public DbSet<LeaderboardEntry> LeaderboardEntries { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<Person> person { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity<Person>(e => e.Property(o => o.Age).HasColumnType("tinyint(1)").HasConversion<short>());
            modelBuilder.Entity<Person>(e => e.Property(o => o.IsPlayer).HasConversion(new BoolToZeroOneConverter<Int16>()).HasColumnType("bit"));
            //modelBuilder.Entity<Category>().HasKey(b => b.Id);
        }
    }
}