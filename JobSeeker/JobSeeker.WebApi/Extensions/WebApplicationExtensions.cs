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
	}
}
