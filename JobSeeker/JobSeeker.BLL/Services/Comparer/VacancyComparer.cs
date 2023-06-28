using JobSeeker.BLL.DTO.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.BLL.Services.Comparer
{
    public static class VacancyComparer
    {
        public static bool Compare(this VacancyDTO oldVacancy, VacancyDTO newVacancy)
        {
            return
                oldVacancy.Link == newVacancy.Link &&
                oldVacancy.CreatedDate == newVacancy.CreatedDate &&
                oldVacancy.Name == newVacancy.Name;
        }
		public static bool Compare(this VacancyDTO oldVacancy, VacancyShortDTO newVacancy)
		{
			return
				oldVacancy.Link == newVacancy.Link &&
				oldVacancy.CreatedDate == newVacancy.CreatedDate &&
				oldVacancy.Name == newVacancy.Name;
		}
	}
}
