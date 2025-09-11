using frontend;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookies")
	.AddCookie("Cookies", options =>
	{
		options.LoginPath = "/Account/Signin";   // where to go if not logged in
		options.LogoutPath = "/Account/Signout"; // optional
		options.AccessDeniedPath = "/Account/Denied"; // optional
	});

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpClient<AuthApiClient>(client =>
{
	client.BaseAddress = new Uri("http://localhost:5000");
});

builder.Services.AddHttpClient("ApiClient", client =>
{
	client.BaseAddress = new Uri("http://localhost:5000"); // your API base URL
});

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
