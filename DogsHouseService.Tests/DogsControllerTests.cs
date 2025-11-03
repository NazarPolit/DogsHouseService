using Xunit;
using Moq; 
using FluentAssertions; 
using DogsHouseService.API.Controllers;
using DogsHouseService.Application.Interfaces;
using DogsHouseService.Application.DTOs;
using DogsHouseService.Domain.Entities;
using FluentValidation; 
using FluentValidation.Results; 
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogsHouseService.Tests
{
	public class DogsControllerTests
	{
		private readonly Mock<IDogRepository> _mockRepo;
		private readonly Mock<IValidator<CreateDogRequest>> _mockValidator;
		private readonly Mock<IMapper> _mockMapper;

		private readonly DogsController _controller;

		public DogsControllerTests()
		{
			_mockRepo = new Mock<IDogRepository>();
			_mockValidator = new Mock<IValidator<CreateDogRequest>>();
			_mockMapper = new Mock<IMapper>();

			_controller = new DogsController(
				_mockRepo.Object,
				_mockValidator.Object,
				_mockMapper.Object 
			);
		}

		[Fact]
		public async Task CreateDog_WhenValidationFails_ShouldReturnBadRequest()
		{
			var testRequest = new CreateDogRequest();

			var validationResult = new ValidationResult(new[]
			{
				new ValidationFailure("Name", "Name is required")
			});

			_mockValidator
				.Setup(v => v.ValidateAsync(testRequest, It.IsAny<CancellationToken>()))
				.ReturnsAsync(validationResult);

			var result = await _controller.CreateDog(testRequest);

			result.Should().BeOfType<BadRequestObjectResult>();

			_mockRepo.Verify(r => r.CreateAsync(It.IsAny<Dog>()), Times.Never);
		}

		[Fact]
		public async Task CreateDog_WhenNameAlreadyExists_ShouldReturnConflict()
		{
			var testRequest = new CreateDogRequest { Name = "Luna"};

			_mockValidator
				.Setup(v => v.ValidateAsync(testRequest, It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ValidationResult());

			_mockRepo
				.Setup(r => r.DogExistsAsync(testRequest.Name))
				.ReturnsAsync(true);

			var result = await _controller.CreateDog(testRequest);

			result.Should().BeOfType<ConflictObjectResult>();

			_mockRepo.Verify(r => r.CreateAsync(It.IsAny<Dog>()), Times.Never);
		}

		[Fact]
		public async Task CreateDog_WhenRequestIsValid_ShouldReturnCreateAtAction()
		{
			var testRequest = new CreateDogRequest { Name = "Mia" };
			var mappedDog = new Dog { Name = "Mia" };
			var createdDog = new Dog {Id = 1, Name = "Mia" };

			_mockValidator
				.Setup(v => v.ValidateAsync(testRequest, It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ValidationResult());

			_mockRepo
				.Setup(r => r.DogExistsAsync(testRequest.Name))
				.ReturnsAsync(false);

			_mockMapper
				.Setup(m => m.Map<Dog>(testRequest))
				.Returns(mappedDog);

			_mockRepo
				.Setup(r => r.CreateAsync(mappedDog))
				.ReturnsAsync(createdDog);

			var result = await _controller.CreateDog(testRequest);

			var actionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;

			_mockRepo.Verify(r => r.CreateAsync(It.IsAny<Dog>()), Times.Once);

			actionResult.Value.Should().Be(createdDog);
		}

		[Fact]
		public async Task GetDogs_WhenCalled_ReturnsOkWithDogList()
		{
			var fakeDogs = new List<Dog> { new Dog { Id = 1, Name = "Mia" } };

			_mockRepo
				.Setup(r => r.GetAllAsync(
					It.IsAny<int>(), 
					It.IsAny<int>(), 
					It.IsAny<string>(),
					It.IsAny<string>()
					)
				)
				.ReturnsAsync(fakeDogs);

			var result = await _controller.GetAllDogs();

			var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

			okResult.Value.Should().Be(fakeDogs);
		}


	}
}
