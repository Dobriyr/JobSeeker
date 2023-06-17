using AutoMapper;
using FluentResults;
using JobSeeker.BLL.DTO.Vacancy;
using JobSeeker.BLL.MediatR.ResultVariations;
using JobSeeker.DAL.Repositories.Interfaces.Base;
using MediatR;

namespace JobSeeker.BLL.MediatR.Vacancy.GetAllTodayBySiteId
{
	public class GetAllTodayVacanciesBySiteIdHandler : IRequestHandler<GetAllTodayVacanciesBySiteIdQuery, Result<IEnumerable<VacancyDTO>>>
	{
		private readonly IRepositoryWrapper _repositoryWrapper;
		private readonly IMapper _mapper;

		public GetAllTodayVacanciesBySiteIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
		{
			_repositoryWrapper = repositoryWrapper;
			_mapper = mapper;
		}

		public async Task<Result<IEnumerable<VacancyDTO>>> Handle(GetAllTodayVacanciesBySiteIdQuery request, CancellationToken cancellationToken)
		{
		
			var vacancies = await _repositoryWrapper.VacancyRepository
				.GetAllAsync(predicate: x => x.CreatedDate == DateTime.Today);
			
			if (vacancies == null || vacancies?.Count() == 0)
			{
				return new NullResult<IEnumerable<VacancyDTO>>();
			}

			var vacanciesDTO = _mapper.Map<IEnumerable<VacancyDTO>>(vacancies);
			return Result.Ok(vacanciesDTO);
		}
	}

}