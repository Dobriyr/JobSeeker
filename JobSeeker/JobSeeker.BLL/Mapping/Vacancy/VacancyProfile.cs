﻿namespace JobSeeker.BLL.Mapping.Vacancy;

using AutoMapper;
using JobSeeker.BLL.DTO.Vacancy;
using JobSeeker.DAL.Entities.Vacancy;

public class VacancyProfile : Profile
{
	public VacancyProfile()
	{
		CreateMap<Vacancy, VacancyDTO>().ReverseMap();
	}
}

