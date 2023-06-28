using Hangfire;
using JobSeeker.BLL.Services.CasheServices;
using JobSeeker.BLL.Services.VacanciesMonitors;
using JobSeeker.DAL.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobSeeker.WebApi.Extensions
{
	public static class WebApplicationExtensions
	{
		public static async Task ApplyMigrations(this WebApplication app)
		{
			try
			{
				var streetcodeContext = app.Services.GetRequiredService<JobSeekerDbContext>();
				await streetcodeContext.Database.MigrateAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine("-----------------------------------------------");
				Console.WriteLine(ex.Message + "An error occured during startup migration");
				Console.WriteLine("-----------------------------------------------");
			}
		}

		public static void AddBackgroundJobs(this WebApplication app)
		{
			RecurringJob.AddOrUpdate<VacancyCasheService>(
					recurringJobId: "clearCache",
					methodCall: vcs => vcs.ClearCache(), Cron.Daily);

			RecurringJob.AddOrUpdate<VacanciesMonitor>(
					"checkNewVacancies",
					vm => vm.CheckForVacancies(), Cron.Minutely());
		}
	}
}
