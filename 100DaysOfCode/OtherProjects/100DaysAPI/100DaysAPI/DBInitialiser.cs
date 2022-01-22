using _100DaysAPI.DbContexts;
using System;
using System.Linq;

namespace _100DaysAPI
{
    public static class DbInitializer
    {
        public static void Initialize(BabyDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.MilkLogs.Any())
            {
                return;   // DB has been seeded
            }
        }
    }
}
