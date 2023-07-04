using JobSeeker.BLL.Dto.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.BLL.Services.Comparer
{
    public static class VacancyComparer
    {
        public static bool Compare(this VacancyDto oldVacancy, VacancyDto newVacancy)
        {
            return
                oldVacancy.Link == newVacancy.Link &&
                oldVacancy.CreatedDate == newVacancy.CreatedDate &&
                oldVacancy.Name == newVacancy.Name;
        }
		public static bool Compare(this VacancyDto oldVacancy, VacancyShortDto newVacancy)
		{
			return
				oldVacancy.Link == newVacancy.Link &&
				oldVacancy.CreatedDate == newVacancy.CreatedDate &&
				oldVacancy.Name == newVacancy.Name;
		}
	}
}
