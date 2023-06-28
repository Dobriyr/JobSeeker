using JobSeeker.DAL.Entities.Vacancy;
using Microsoft.EntityFrameworkCore;

namespace JobSeeker.DAL.Persistence
{
	public class JobSeekerDbContext : DbContext
	{ 
		public JobSeekerDbContext()
		{
		}

		public JobSeekerDbContext(DbContextOptions<JobSeekerDbContext> options)
			: base(options)
		{
		}
		public DbSet<Vacancy> Vacancies { get; set; }
	}
}
