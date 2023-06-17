using AutoMapper;
using JobSeeker.BLL.DTO.Vacancy;
using JobSeeker.BLL.MediatR.Vacancy.GetAllTodayBySiteId;
using JobSeeker.BLL.Services.Comparer;
using JobSeeker.BLL.Services.Parsers;
using JobSeeker.BLL.Services.Parsers.Base;
using JobSeeker.DAL.Entities.Vacancy;
using JobSeeker.DAL.Repositories.Interfaces.Base;
using Microsoft.IdentityModel.Tokens;

namespace JobSeeker.BLL.Services.NewVacanciesMonitor
{
	public class VacanciesMonitor
	{
		private readonly IRepositoryWrapper _repositoryWrapper;
		private readonly IMapper _mapper;
		private readonly IParser _parser;
		private List<VacancyDTO> _oldVacancies;

		public VacanciesMonitor(IRepositoryWrapper repository, IMapper mapper, IParser parser)
		{
			_repositoryWrapper = repository;
			_mapper = mapper;
			_parser = parser;
			_oldVacancies = GetOldVacancies().ToList();
		}

		public async Task CheckForVacancies()
		{

			Console.WriteLine("------------check-for-vacancies--------------");

			var newVacancies = _parser.Parse();
			
			if (!newVacancies.IsNullOrEmpty())
			{
				var vacanciesToAdd = newVacancies.Where(newVacancy => !_oldVacancies.Any(oldVacancy => newVacancy.Compare(oldVacancy)));
				
				if (!vacanciesToAdd.IsNullOrEmpty())
				{
					var vacancies = _mapper.Map<IEnumerable<Vacancy>>(vacanciesToAdd);
					await _repositoryWrapper.VacancyRepository.CreateRangeAsync(vacancies);
					
					_repositoryWrapper.SaveChanges();
					Console.WriteLine($"Add {vacancies.Count()} vacancies");

				}
			}
		}

		private IEnumerable<VacancyDTO> GetOldVacancies()
		{
			var Vacancies = _repositoryWrapper.VacancyRepository
				.GetAllAsync(/*predicate: s => s.CreatedDate == DateTime.Today*/).Result;

			var VacanciesDTO = _mapper.Map<List<VacancyDTO>>(Vacancies);

			return VacanciesDTO;
		}
	}
}
