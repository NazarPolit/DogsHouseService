using DogsHouseService.Application.Interfaces;
using DogsHouseService.Domain.Entities;
using DogsHouseService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
	}
}
