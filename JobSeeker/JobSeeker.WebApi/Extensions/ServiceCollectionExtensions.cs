using JobSeeker.DAL.Persistence;
using JobSeeker.DAL.Repositories.Interfaces.Base;
using JobSeeker.DAL.Repositories.Realizations.Base;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.OpenApi.Models;
using Hangfire;
using JobSeeker.BLL.Services.Parsers.Base;
using JobSeeker.BLL.Services.Parsers;
using JobSeeker.BLL.Services.VacanciesMonitors;
using JobSeeker.BLL.Services.CasheServices;

namespace JobSeeker.WebApi.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddRepositoryServices(this IServiceCollection services)
		{
			services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
		}
		public static void AddCustomServices(this IServiceCollection services)
		{

			services.AddRepositoryServices();
			AppDomain.CurrentDomain.Load("JobSeeker.Bll"); // bll add another way 
			var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

			services.AddAutoMapper(currentAssemblies);
			services.AddMediatR(currentAssemblies);
			services.AddMemoryCache();

			services.AddSingleton<SeedDataExtension>();
			services.AddScoped<IParser, DjiniParser>();
			services.AddSingleton<VacanciesMonitor>();
			services.AddSingleton<IVacancyCacheService, VacancyCasheService>();
			// other services...)
		}
		public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");

			services.AddDbContext<JobSeekerDbContext>(options => {
				options.UseSqlServer(connectionString, opt =>
				{
					opt.MigrationsAssembly(typeof(JobSeekerDbContext).Assembly.GetName().Name);
					opt.MigrationsHistoryTable("__EFMigrationsHistory", schema: "entity_framework");
				});
			});

			services.AddHangfire(config =>
			{
				config.UseSqlServerStorage(connectionString);
			});
			services.AddHangfireServer();

			services.AddControllers();

		}

		public static void AddSwaggerServices(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(opt =>
			{
				opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApi", Version = "v1" });
				opt.CustomSchemaIds(x => x.FullName);
			});
		}
	}
}
