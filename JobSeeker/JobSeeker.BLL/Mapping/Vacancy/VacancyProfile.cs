namespace JobSeeker.BLL.Mapping.Vacancy;

using AutoMapper;
using JobSeeker.BLL.Dto.Vacancy;
using JobSeeker.DAL.Entities.Vacancy;

public class VacancyProfile : Profile
{
	public VacancyProfile()
	{
		CreateMap<Vacancy, VacancyDto>().ReverseMap();
		// .ForMember(x => x.CreatedDate, conf => conf.MapFrom(x => DateOnly.FromDateTime(x.CreatedDate)));

		CreateMap<Vacancy, VacancyShortDto>();//.ForMember(x => x.CreatedDate, conf => conf.MapFrom(x => DateOnly.FromDateTime(x.CreatedDate)));
		;

		CreateMap<VacancyDto, VacancyShortDto>();
	}
}

