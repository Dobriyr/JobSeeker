using AutoMapper;
using FluentResults;
using JobSeeker.BLL.DTO.Vacancy;
using JobSeeker.BLL.MediatR.ResultVariations;
using JobSeeker.DAL.Repositories.Interfaces.Base;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace JobSeeker.BLL.MediatR.Vacancy.GetAll
{
	public class GetAllVacanciesHandler : IRequestHandler<GetAllVacanciesQuery, Result<IEnumerable<VacancyDto>>>
	{
		private readonly IRepositoryWrapper _repositoryWrapper;
		private readonly IMapper _mapper;

		public GetAllVacanciesHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
		{
			_repositoryWrapper = repositoryWrapper;
			_mapper = mapper;
		}

		public async Task<Result<IEnumerable<VacancyDto>>> Handle(GetAllVacanciesQuery request, CancellationToken cancellationToken)
		{
			var vacancies = await _repositoryWrapper.VacancyRepository.GetAllAsync();
			if (vacancies.IsNullOrEmpty())
			{
				return new NullResult<IEnumerable<VacancyDto>>();
			}
			
			var vacanciesDTO = _mapper.Map<IEnumerable<VacancyDto>>(vacancies);
			return Result.Ok(vacanciesDTO);
		}
	}
}
