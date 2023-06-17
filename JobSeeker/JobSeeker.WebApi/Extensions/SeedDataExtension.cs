using AutoMapper;
using JobSeeker.BLL.DTO.Vacancy;
using JobSeeker.BLL.Services.Parsers;
using JobSeeker.BLL.Services.Parsers.Base;
using JobSeeker.DAL.Entities.Vacancy;
using JobSeeker.DAL.Persistence;
using JobSeeker.DAL.Repositories.Interfaces.Base;

namespace JobSeeker.WebApi.Extensions
{
    public class SeedDataExtension
	{
		private readonly IRepositoryWrapper _wrapper;
		private readonly IMapper _mapper;

		public SeedDataExtension(IRepositoryWrapper wrapper, IMapper mapper)
		{
			_wrapper = wrapper;
			_mapper = mapper;
		}

		private bool _seed = false;

		public static async Task SeedData(IHost app)
		{
			var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

			using (var scope = scopedFactory?.CreateScope())
			{
				var service = scope?.ServiceProvider.GetService<SeedDataExtension>();
				try
				{
					if (service != null)
					{
						await service!.Seed();
					}
					else
					{
						Console.WriteLine("service is null");
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

		public async Task Seed()
		{
			if (!_wrapper.VacancyRepository.Any())
			{
				await SeedVacancies();
				_seed = true;
			}
			if (_seed)
			{
				_wrapper.SaveChanges();
			}
		}
		private async Task SeedVacancies()
		{
			IParser parser = new DjiniParser();
			var vacancies = _mapper.Map<IEnumerable<Vacancy>>(parser.Parse());
			await _wrapper.VacancyRepository.CreateRangeAsync(vacancies);
		}
	}
}
