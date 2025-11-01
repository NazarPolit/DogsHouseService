using AutoMapper;
using DogsHouseService.Application.DTOs;
using DogsHouseService.Application.Interfaces;
using DogsHouseService.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace DogsHouseService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DogsController : ControllerBase
	{
		private readonly IDogRepository _dogRepository;
		private readonly IValidator<CreateDogRequest> _createDogValidator;
		private readonly IMapper _mapper;

		public DogsController(IDogRepository dogRepository,
							  IValidator<CreateDogRequest> createDogValidator,
							  IMapper mapper)
        {
			_dogRepository = dogRepository;
			_createDogValidator = createDogValidator;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Dog>))]
		[Route("/dogs")]
		public async Task<IActionResult> GetAllDogs()
		{
			var dogs = await _dogRepository.GetAllAsync();
			return Ok(dogs);
		}

		[HttpPost]
		[Route("/dog")] 
		[ProducesResponseType(typeof(Dog), 201)] 
		[ProducesResponseType(400)]
		[ProducesResponseType(409)]
		public async Task<IActionResult> CreateDog([FromBody] CreateDogRequest request)
		{
			var validationResult = await _createDogValidator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				return BadRequest(validationResult.ToDictionary());
			}

			if(await _dogRepository.DogExistsAsync(request.Name))
			{
				return Conflict(new { message = $"A dog with name {request.Name} already exists." });
			}

			var newDog = _mapper.Map<Dog>(request);

			var createdDog = await _dogRepository.CreateAsync(newDog);

			return CreatedAtAction(nameof(GetAllDogs), new { name = createdDog.Name }, createdDog);
		}
	}
}
