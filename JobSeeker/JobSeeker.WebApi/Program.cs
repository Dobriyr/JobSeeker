using Hangfire;
using JobSeeker.BLL.Services.NewVacanciesMonitor;
using JobSeeker.BLL.Services.Parsers;
using JobSeeker.WebApi.Extensions;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureApplication();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSwaggerServices();
builder.Services.AddCustomServices();

var app = builder.Build();


if (app.Environment.EnvironmentName == "Local")
{
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
	await app.ApplyMigrations();

	await SeedDataExtension.SeedData(app);
}
else
{
	app.UseHsts();
}
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

// BackgroundJob.Schedule<VacanciesMonitor>(
//	 wp => wp.CheckForVacancies(), TimeSpan.FromMinutes(1));

app.Run();
