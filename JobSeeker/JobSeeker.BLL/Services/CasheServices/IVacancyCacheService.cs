using JobSeeker.BLL.DTO.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.BLL.Services.CasheServices
{
	public interface IVacancyCacheService
	{
		public void ClearCache();
		public void InitializeCache();
		public void AddVacanciesToCache(IEnumerable<VacancyShortDTO> vacanciesToAdd);
		public IEnumerable<VacancyShortDTO> GetCachedVacancies();
	}
}
