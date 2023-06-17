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
	
		/*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{		
			optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=JobSeekerDb;User Id=sa;Password=Admin@1234;MultipleActiveResultSets=true");
		}*/

		public DbSet<Vacancy> Vacancies { get; set; }
	}
}
