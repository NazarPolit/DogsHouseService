using Xunit;
using FluentAssertions;
using FluentValidation.TestHelper; 
using DogsHouseService.Application.DTOs;
using DogsHouseService.Application.Validators;

namespace DogsHouseService.Tests
{
    public class CreateDogRequestValidatorTests
    {
        private readonly CreateDogRequestValidator _validator;
        public CreateDogRequestValidatorTests()
        {
            _validator = new CreateDogRequestValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Name_Is_Null_Or_Empty(string name)
        {
            var request = new CreateDogRequest { Name = name };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(d => d.Name);

        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Too_Long()
        {
            var request = new CreateDogRequest { Name = new string('a', 101) };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(d => d.Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Should_Have_Error_When_Weight_Is_Zero_Or_Negative(decimal weight)
        {
            var request = new CreateDogRequest { Weight = weight };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(d => d.Weight);
        }

        [Fact]
        public void Should_Have_Error_When_TailLength_Is_Negative()
        {
            var request = new CreateDogRequest { TailLength = -1 };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(d => d.TailLength);
        }

        [Fact]
        public void Should_Have_Error_When_TailLength_Is_Zero()
        {
            var request = new CreateDogRequest { TailLength = 0 };

            var result = _validator.TestValidate(request);

            result.ShouldNotHaveValidationErrorFor(d => d.TailLength);
        }

        [Fact]
        public void Should_Pass_When_Request_Is_Valid()
        {
            var request = new CreateDogRequest
            {
                Name = "Luna",
                Color = "brown & black",
                TailLength = 40,
                Weight = 40
            };

            var result = _validator.TestValidate(request); 

            result.ShouldNotHaveAnyValidationErrors();
        }

	}
}
