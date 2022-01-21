using System;
using Microsoft.EntityFrameworkCore;

namespace _100DaysAPI.DbContexts
{
	public class BabyDbContext : DbContext
	{
		public BabyDbContext(DbContextOptions<BabyDbContext> options)
		{	
			var x = options;
		}
	}
}