using AutoMapper;
using JobSeeker.BLL.Dto.Vacancy;
using JobSeeker.BLL.Services.CasheServices;
using JobSeeker.BLL.Services.Comparer;
using JobSeeker.BLL.Services.Parsers;
using JobSeeker.BLL.Services.Parsers.Base;
using JobSeeker.DAL.Entities.Vacancy;
using JobSeeker.DAL.Repositories.Interfaces.Base;
using Microsoft.IdentityModel.Tokens;

namespace JobSeeker.BLL.Services.VacanciesMonitors
{
	public class VacanciesMonitor
	{
		private readonly IRepositoryWrapper _repositoryWrapper;
		private readonly IMapper _mapper;
		private readonly IParser _parser;
		private readonly IVacancyCacheService _vacancyCache;

		public VacanciesMonitor(IRepositoryWrapper repository, IMapper mapper, IParser parser, IVacancyCacheService memoryCache)
		{
			_repositoryWrapper = repository;
			_mapper = mapper;
			_parser = parser;
			_vacancyCache = memoryCache;
		}

		public async Task CheckForVacancies()
		{
			var oldVacancies = _vacancyCache.GetCachedVacancies();
			var newVacancies = GetNewVacancies();

			if (!newVacancies.IsNullOrEmpty())
			{
				var vacanciesToAdd = newVacancies.Where(newVacancy =>
					!oldVacancies.Any(oldVacancy => newVacancy.Compare(oldVacancy)));

				if (!vacanciesToAdd.IsNullOrEmpty())
				{
					int count = vacanciesToAdd.Count();
					await SaveVacancies(vacanciesToAdd);
					Console.WriteLine($"\nAdded {count} vacancies\n");
				}
			}
		}
		private async Task SaveVacancies(IEnumerable<VacancyDto> vacanciesToAdd)
		{
			var vacanciesForDB = _mapper.Map<IEnumerable<Vacancy>>(vacanciesToAdd);
			var vacanciesForCashe = _mapper.Map<IEnumerable<VacancyShortDto>>(vacanciesToAdd);

			await _repositoryWrapper.VacancyRepository.CreateRangeAsync(vacanciesForDB);
			_repositoryWrapper.SaveChanges();

			_vacancyCache.AddVacanciesToCache(vacanciesForCashe);
		}

		private IEnumerable<VacancyDto> GetNewVacancies()
		{
			return _parser.Parse().Result.Where(x => x.CreatedDate == DateTime.Today);
		}
	}
}
