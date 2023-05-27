using JobSeeker.DAL.Entities.Vacancy;
using JobSeeker.DAL.Persistence;
using JobSeeker.DAL.Repositories.Interfaces.Base;
using JobSeeker.DAL.Repositories.Interfaces.Site;
using JobSeeker.DAL.Repositories.Interfaces.Vacancy;
using JobSeeker.DAL.Repositories.Realizations.Site;
using System.Transactions;

namespace JobSeeker.DAL.Repositories.Realizations.Base
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private readonly JobSeekerDbContext _jobSeekerDbContext;
		private IVacancyRepository _vacancyRepository;
		private ISiteRepository _siteRepository;
		public RepositoryWrapper(JobSeekerDbContext jobSeekerDbContext)
		{
			_jobSeekerDbContext = jobSeekerDbContext;
		}

		public IVacancyRepository VacancyRepository
		{
			get
			{
				if (_vacancyRepository is null)
				{
					_vacancyRepository = new VacancyRepository(_jobSeekerDbContext);
				}

				return _vacancyRepository;
			}
		}
		public ISiteRepository SiteRepository
		{
			get
			{
				if (_siteRepository is null)
				{
					_siteRepository = new SiteRepository(_jobSeekerDbContext);
				}

				return _siteRepository;
			}
		}
		public int SaveChanges()
		{
			return _jobSeekerDbContext.SaveChanges();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _jobSeekerDbContext.SaveChangesAsync();
		}

		public TransactionScope BeginTransaction()
		{
			return new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
		}
	}
}
