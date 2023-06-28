namespace JobSeeker.BLL.Mapping.Vacancy;

using AutoMapper;
using JobSeeker.BLL.DTO.Vacancy;
using JobSeeker.DAL.Entities.Vacancy;

public class VacancyProfile : Profile
{
	public VacancyProfile()
	{
		CreateMap<VacancyDTO, Vacancy>()
			.ReverseMap();

		CreateMap<Vacancy, VacancyShortDTO>();
		
		CreateMap<VacancyDTO, VacancyShortDTO>();
	}
}

