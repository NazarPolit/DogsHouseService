using DogsHouseService.Application.DTOs;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Application.Validators
{
	public class CreateDogRequestValidator: AbstractValidator<CreateDogRequest>
	{
		public CreateDogRequestValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Name is required.")
				.MaximumLength(100);

			RuleFor(x => x.Color)
				.NotEmpty().WithMessage("Color is required.")
				.MaximumLength(100);

			RuleFor(x => x.TailLength)
				.GreaterThanOrEqualTo(0).WithMessage("Tail length cannot be a negative number.");

			RuleFor(x => x.Weight)
				.GreaterThan(0).WithMessage("Weight must be greater than 0.");
		}
	}
}
