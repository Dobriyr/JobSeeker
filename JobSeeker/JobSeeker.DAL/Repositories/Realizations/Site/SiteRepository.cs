namespace JobSeeker.DAL.Repositories.Realizations.Site;

using JobSeeker.DAL.Repositories.Realizations.Base;
using DAL.Entities.Site;
using JobSeeker.DAL.Repositories.Interfaces.Site;
using JobSeeker.DAL.Persistence;

public class SiteRepository : RepositoryBase<Site>, ISiteRepository
{
	public SiteRepository(JobSeekerDbContext dbContext)
		: base(dbContext)
	{
	}
}

