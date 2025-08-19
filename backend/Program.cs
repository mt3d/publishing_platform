using backend.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// TODO: Configure connection string and database provider
builder.Services.AddDbContext<PlatformContext>(options =>
{
	// TODO: Add the ability to use other providers
	options.UseSqlServer(builder.Configuration["ConnectionStrings:PlatformConnection"]);
});

// TODO: Add Localization

builder.Services.AddSwaggerGen(opts =>
{
	// TODO: Add Security Definition to describe how the api is protected
	// TODO: Does the API support Non Nullable Reference types?
	// TODO: AddSecurityRequirement()
	opts.SwaggerDoc("v1", new OpenApiInfo { Title = "Publishing_Platform", Version = "v0.1" });
});

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

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Publishing_Platform"); });
}

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<PlatformContext>().Database.EnsureCreated();
}

app.Run();