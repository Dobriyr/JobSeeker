using JobSeeker.BLL.MediatR.Vacancy.GetAll;
using JobSeeker.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JobSeeker.WebApi.Controllers.Vacancy
{
	public class VacancyController : BaseApiController
	{
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return HandleResult(await Mediator.Send(new GetAllVacanciesQuery()));
		}
	}
}
