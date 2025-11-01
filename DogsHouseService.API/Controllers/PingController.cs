using Microsoft.AspNetCore.Mvc;

namespace DogsHouseService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PingController : ControllerBase
	{
		[HttpGet]
		[Route("/ping")]
		public IActionResult GetPing()
		{
			return Ok("Dogshouseservice.Version1.0.1");
		}
	}
}
