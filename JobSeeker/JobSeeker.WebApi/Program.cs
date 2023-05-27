using Hangfire;
using JobSeeker.WebApi.Extensions;

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

	SeedDataExtension.SeedData(app);
}
else
{
	app.UseHsts();
}
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

/*BackgroundJob.Schedule<DjiniParser>(
	 wp => wp.AddNew(), TimeSpan.FromMinutes(1));*/

app.Run();
