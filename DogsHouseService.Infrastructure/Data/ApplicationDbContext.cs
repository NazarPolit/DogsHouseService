using Microsoft.EntityFrameworkCore;
using DogsHouseService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {  
        }

        public DbSet<Dog> Dogs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Dog>()
				.HasIndex(d => d.Name)
				.IsUnique();
		}
	}
}
