using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Application.DTOs
{
    public class CreateDogRequest
	{
		public string Name { get; set; } = string.Empty;
		public string Color { get; set; } = string.Empty;
		public decimal TailLength { get; set; }
		public decimal Weight { get; set; }
	}
}
