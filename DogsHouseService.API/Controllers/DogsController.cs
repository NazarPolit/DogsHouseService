using DogsHouseService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DogsHouseService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DogsController : ControllerBase
	{
		private readonly IDogRepository _dogRepository;

		public DogsController(IDogRepository dogRepository)
        {
			_dogRepository = dogRepository;
		}

		[HttpGet]
		[Route("/dogs")]
		public async Task<IActionResult> GetAllDogs()
		{
			var dogs = await _dogRepository.GetAllAsync();
			return Ok(dogs);
		}
	}
}
