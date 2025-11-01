using DogsHouseService.Application.Interfaces;
using DogsHouseService.Domain.Entities;
using DogsHouseService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Infrastructure.Repositories
{
	public class DogRepository : IDogRepository
	{
        private readonly ApplicationDbContext _context;
        public DogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

		public async Task<IEnumerable<Dog>> GetAllAsync()
		{
			return await _context.Dogs.ToListAsync();
		}

		public async Task<Dog> CreateAsync(Dog dog)
		{
			await _context.Dogs.AddAsync(dog);

			await Save();

			return dog;

		}

		public async Task<bool> Save()
		{
			var saved = await _context.SaveChangesAsync();
			return saved > 0;
		}

		public async Task<bool> DogExistsAsync(string name)
		{
			return await _context.Dogs
				.AnyAsync(dog => dog.Name.ToLower() == name.ToLower());
		}
	}
}
