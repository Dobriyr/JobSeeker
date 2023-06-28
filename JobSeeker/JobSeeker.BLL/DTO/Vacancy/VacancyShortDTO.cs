using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.BLL.DTO.Vacancy
{
	public class VacancyShortDto
	{
		public string Name { get; set; } = string.Empty;
		public string Link { get; set; } = string.Empty;
		public DateTime CreatedDate { get; set; }
	}
}
