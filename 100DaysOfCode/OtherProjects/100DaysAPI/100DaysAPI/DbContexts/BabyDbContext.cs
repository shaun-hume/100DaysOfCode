using System;
using Microsoft.EntityFrameworkCore;

namespace _100DaysAPI.DbContexts
{
	public class BabyDbContext : DbContext
	{
		public DbSet<MilkLog> MilkLogs { get; set; }
		public DbSet<SleepLog> SleepLogs { get; set; }
		public DbSet<ExerciseLog> ExerciseLogs { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
	}
}