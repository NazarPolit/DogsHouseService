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
		Task<IEnumerable<Dog>> GetPagedAsync(int pageNumber, int pageSize);
	}
}
