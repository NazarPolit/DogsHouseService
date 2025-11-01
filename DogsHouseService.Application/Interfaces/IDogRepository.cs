using DogsHouseService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Application.Interfaces
{
	public interface IDogRepository
	{
		Task<IEnumerable<Dog>> GetAllAsync();
		Task<Dog> CreateAsync(Dog dog);
		Task<bool> Save();
		Task<bool> DogExistsAsync(string name);
	}
}
