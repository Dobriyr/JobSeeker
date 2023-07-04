using MediatR;
using FluentResults;
using JobSeeker.BLL.Dto.Vacancy;

namespace JobSeeker.BLL.MediatR.Vacancy.GetAll
{
	public record GetAllVacanciesQuery() : IRequest<Result<IEnumerable<VacancyDto>>>;
}
