using DogsHouseService.Domain.Entities;
using DogsHouseService.Infrastructure.Data;
using DogsHouseService.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Tests
{
	public class DogRepositoryTests
	{
		private readonly ApplicationDbContext _context;
		private readonly DogRepository _repository;

        public DogRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new DogRepository(_context);
        }

        private async Task SeedData()
        {
            var dogs = new List<Dog>()
            {
                new Dog {Name = "Luna", Weight = 40, TailLength = 40},
				new Dog {Name = "Mia", Weight = 1.5M, TailLength = 12},
				new Dog {Name = "Oskar", Weight = 20, TailLength = 20.5M}
			};

            await _context.Dogs.AddRangeAsync(dogs);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAllAsync_Should_Sort_By_Weight_Descending()
        {
            await SeedData();

            var result = await _repository.GetAllAsync(
                pageNumber: 1,
                pageSize: 3,
                attribute: "weight",
                order: "desc");

            var dogList = result.ToList();

            dogList.Should().BeInDescendingOrder(d => d.Weight);
            dogList[0].Name.Should().Be("Luna");
            dogList[1].Name.Should().Be("Oskar");
        }

        [Fact]
        public async Task GetAllAsync_Should_Pagginate_Correcly()
        {
            await SeedData();

			var result = await _repository.GetAllAsync(
				pageNumber: 2,
				pageSize: 1,
				attribute: null,
				order: null);

            var dogList = result.ToList(); 

            dogList.Should().HaveCount(1);
			dogList[0].Name.Should().Be("Mia");
		}

        [Fact]
        public async Task CreateAsync_Should_AddDog_And_ReturnsWithId()
        {
            var newDog = new Dog { Name = "Ada", Color = "Gray", Weight = 45, TailLength = 48 };

            var createdDog = await _repository.CreateAsync(newDog);
            var dogFromDb = await _context.Dogs.FindAsync(createdDog.Id);

            createdDog.Id.Should().BeGreaterThan(0);

            dogFromDb.Should().NotBeNull();
            dogFromDb.Name.Should().Be("Ada");
        }

        [Fact]
		public async Task Save_Should_PersistChanges_ToDatabase()
		{
			_context.Dogs.Add(new Dog { Name = "Chop", Weight = 2, TailLength = 9 });

            var result = await _repository.Save();

            result.Should().BeTrue();

            var count = await _context.Dogs.CountAsync();
            count.Should().Be(1);
		}

	}
}
