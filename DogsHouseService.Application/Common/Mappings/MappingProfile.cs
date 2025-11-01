using AutoMapper;
using DogsHouseService.Application.DTOs;
using DogsHouseService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Application.Common.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			CreateMap<CreateDogRequest, Dog>();
			CreateMap<Dog, CreateDogRequest>();
		}
	}
}
