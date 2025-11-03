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

		public async Task<IEnumerable<Dog>> GetAllAsync(
			int pageNumber, int pageSize,
			string? attribute, string? order)
		{
			var query = _context.Dogs.AsQueryable();

			if(!string.IsNullOrEmpty(attribute))
			{
				var isDescending = "desc".Equals(order, StringComparison.OrdinalIgnoreCase);

				query = attribute.ToLower() switch
				{
					"name" => isDescending
						? query.OrderByDescending(d => d.Name) 
						: query.OrderBy(d => d.Name),

					"color" => isDescending
						? query.OrderByDescending(d => d.Color)
						: query.OrderBy(d => d.Color),

					"tail_length" => isDescending
						? query.OrderByDescending(d => d.TailLength)
						: query.OrderBy(d => d.TailLength),

					"weight" => isDescending
						? query.OrderByDescending(d => d.Weight)
						: query.OrderBy(d => d.Weight),

					_ => query.OrderBy(d => d.Id),
				};
				
			}
			else
			{
				query = query.OrderBy(d => d.Id);
			}

			var pagedQuery = query
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize);

			return await pagedQuery.ToListAsync();
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
