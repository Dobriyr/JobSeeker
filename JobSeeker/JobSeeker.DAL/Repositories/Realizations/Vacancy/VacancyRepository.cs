using JobSeeker.DAL.Persistence;
using JobSeeker.DAL.Repositories.Interfaces.Vacancy;
using JobSeeker.DAL.Repositories.Realizations.Base;
namespace JobSeeker.DAL.Entities.Vacancy;

public class VacancyRepository : RepositoryBase<Vacancy>, IVacancyRepository
{
	public VacancyRepository(JobSeekerDbContext context)
		: base(context)
	{
	}
}