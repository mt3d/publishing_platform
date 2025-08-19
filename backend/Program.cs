using Azure.Identity;
using backend.Logic;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// TODO: Add DbContext

// TODO: Configure connection string and database provider
builder.Services.AddDbContext<PlatformContext>(options =>
{
	// TODO: Add the ability to use other providers
	options.UseSqlServer(builder.Configuration["ConnectionStrings:PlatformConnection"]);
});

// TODO: Add Localization

// TODO: Add SwaggerGen

// TODO: Add Cors

builder.Services.AddControllers(opts =>
{
});

// TODO: Add custom services here

// TODO: Add JWT

var app = builder.Build();

// TODO: Use error handling

// TODO: Use Cors

// TODO Use Authentication

app.MapControllers();

// TODO: Use Swagger

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<PlatformContext>().Database.EnsureCreated();
}

app.Run();