using MediatR;
using FluentResults;
using JobSeeker.BLL.DTO.Vacancy;

namespace JobSeeker.BLL.MediatR.Vacancy.GetAll
{
	public record GetAllVacanciesQuery() : IRequest<Result<IEnumerable<VacancyDTO>>>;
}
