namespace JobSeeker.BLL.Mapping.Vacancy;

using AutoMapper;
using JobSeeker.BLL.Dto.Vacancy;
using JobSeeker.DAL.Entities.Vacancy;

public class VacancyProfile : Profile
{
	public VacancyProfile()
	{
		CreateMap<VacancyDto, Vacancy>()
			.ReverseMap();

		CreateMap<Vacancy, VacancyShortDto>();
		
		CreateMap<VacancyDto, VacancyShortDto>();
	}
}

