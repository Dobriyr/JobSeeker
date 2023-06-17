using JobSeeker.DAL.Repositories.Interfaces.Vacancy;
using System.Transactions;

namespace JobSeeker.DAL.Repositories.Interfaces.Base
{
	public interface IRepositoryWrapper
	{
		IVacancyRepository VacancyRepository { get; }
		public int SaveChanges();
		public Task<int> SaveChangesAsync();
		public TransactionScope BeginTransaction();
	}
}
