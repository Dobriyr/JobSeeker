using JobSeeker.BLL.Services.Parsers;
using JobSeeker.DAL.Entities.Site;
using JobSeeker.DAL.Persistence;

namespace JobSeeker.WebApi.Extensions
{
	public class SeedDataExtension
	{
		private readonly JobSeekerDbContext _context;

		public SeedDataExtension(JobSeekerDbContext context)
		{
			_context = context;
		}
		private bool _seed = false;

		public static void SeedData(IHost app)
		{
			var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

			using (var scope = scopedFactory.CreateScope())
			{
				var service = scope.ServiceProvider.GetService<SeedDataExtension>();
				if (service == null)
				{
					Console.WriteLine("something go wrong TEACH ME TO PROGRAM THIS APP))");
				}
				service?.Seed();
			}
		}

		public void Seed()
		{
			if (!_context.Sites.Any())
			{
				SeedSites();
				_seed = true;
			}

			if (!_context.Vacancies.Any())
			{
				SeedVacancies();
				_seed = true;
			}
			if (_seed)
			{
				_context.SaveChanges();
			}
		}

		private void SeedSites()
		{
			var sites = new List<Site>()
				{
					new Site { Link = "https://djinni.co/" },
					new Site { Link = "https://dou.ua/" },
					new Site { Link = "https://rabota.ua/" }
				};

			_context.Sites.AddRange(sites);
			
		}
		private void SeedVacancies()
		{
			Parser parser = new DjiniParser("");
			var vacancies = parser.Parse();
			_context.Vacancies.AddRange(vacancies);
		}
	}
}
