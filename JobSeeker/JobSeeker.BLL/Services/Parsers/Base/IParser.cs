using JobSeeker.BLL.DTO.Vacancy;
using JobSeeker.DAL.Entities.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.BLL.Services.Parsers.Base
{
	public interface IParser
	{
		public Task<IEnumerable<VacancyDto>> Parse();
	}
}
