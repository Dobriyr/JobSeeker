namespace JobSeeker.BLL.Mapping.Site;

using AutoMapper;
using JobSeeker.BLL.DTO.Site;
using JobSeeker.DAL.Entities.Site;

public class SiteProfile : Profile
{
	public SiteProfile()
	{
		CreateMap<Site, SiteDTO>().ReverseMap();
	}
}

