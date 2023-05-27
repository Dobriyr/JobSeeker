using FluentResults;
using JobSeeker.BLL.DTO.Vacancy;
using MediatR;

namespace JobSeeker.BLL.MediatR.Vacancy.GetAllTodayBySiteId
{
	public record class GetAllTodayVacanciesBySiteIdQuery(int siteId) : IRequest<Result<IEnumerable<VacancyDTO>>>;
}
