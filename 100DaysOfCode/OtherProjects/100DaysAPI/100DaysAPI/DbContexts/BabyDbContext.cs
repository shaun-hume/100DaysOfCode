using System;
using Microsoft.EntityFrameworkCore;

namespace _100DaysAPI.DbContexts
{
    public class BabyDbContext : DbContext
    {
        //public DbSet<MilkLog> MilkLogs { get; set; }
        //public DbSet<SleepLog> SleepLogs { get; set; }
        //public DbSet<ExerciseLog> ExerciseLogs { get; set; }
        private string _connectionString;
        public BabyDbContext(string nameOrConnectionString)
        {
            _connectionString = nameOrConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}