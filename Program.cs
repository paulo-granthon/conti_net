using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ContiNet.Models;
using Microsoft.OpenApi.Models;
using ContiNet.Services;

var builder = WebApplication.CreateBuilder(args);

builder
  .Services
  .AddControllersWithViews()
  .AddRazorOptions(
    options =>
    {
      options.ViewLocationFormats.Add("/src/Views/{1}/{0}.cshtml");
      options.ViewLocationFormats.Add("/src/Views/Shared/{0}.cshtml");
    }
  );

Env.Load();

string GetEnv(string name) =>
  Environment.GetEnvironmentVariable(name)
  ?? throw new Exception($"Missing environment variable: {name}");

string DbHost = builder.Environment.IsProduction()
  ? GetEnv("DB_HOST")
  : "localhost";

string GenerateDbConnectionString() =>
  $"Server={DbHost},{GetEnv("DB_PORT")};" +
  $"Database={GetEnv("DB_NAME")};" +
  $"User Id={GetEnv("DB_USER")};" +
  $"Password={GetEnv("DB_PASS")};" +
  "Encrypt=True;TrustServerCertificate=True";

builder
  .Services
  .AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(
      GenerateDbConnectionString(),
      builder => builder.MigrationsAssembly("ContiNet")
    )
  );

builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<ContinentService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Country API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Country API v1");
  });
}

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllers();
app.Run();
