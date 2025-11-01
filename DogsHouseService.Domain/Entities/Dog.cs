using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Domain.Entities
{
	public class Dog
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Color { get; set; } = string.Empty;
		public decimal TailLength { get; set; }
		public decimal Weight { get; set; }
	}
}
