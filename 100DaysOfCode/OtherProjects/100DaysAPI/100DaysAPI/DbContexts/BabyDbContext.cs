using System;
using Microsoft.EntityFrameworkCore;

namespace _100DaysAPI.DbContexts
{
    public class BabyDbContext : DbContext
    {
        public DbSet<MilkLog> MilkLogs { get; set; }
        public DbSet<SleepLog> SleepLogs { get; set; }
        public DbSet<ExerciseLog> ExerciseLogs { get; set; }
        public DbSet<PooLog> PooLogs { get; set; }

        private string _connectionString;

        public BabyDbContext(DbContextOptions<BabyDbContext> options) : base(options)
        {
        }
    }

    public class MilkLog
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string MeasurementType { get; set; }
        public string? Comment { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }

    public class SleepLog
    {
        public int ID { get; set; }
        public string? Comment { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }

    public class ExerciseLog
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string? Comment { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }

    public class PooLog
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string? Comment { get; set; }
        public string Colour { get; set; }
        public DateTime OccurrenceTime { get; set; }
    }
}