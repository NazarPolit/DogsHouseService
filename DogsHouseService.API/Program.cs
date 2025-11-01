using Microsoft.EntityFrameworkCore.Design;
using DogsHouseService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using DogsHouseService.Application.Interfaces;
using DogsHouseService.Infrastructure.Repositories;
using FluentValidation;
using DogsHouseService.Application.DTOs;
using DogsHouseService.Application.Validators;

namespace DogsHouseService.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

			builder.Services.AddScoped<IDogRepository, DogRepository>();

			builder.Services.AddFluentValidationAutoValidation();
			builder.Services.AddValidatorsFromAssemblyContaining<CreateDogRequestValidator>();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration
					.GetConnectionString("DefaultConnection"));
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
