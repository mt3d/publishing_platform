var builder = WebApplication.CreateBuilder(args);

// TODO: Add DbContext

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

app.Run();