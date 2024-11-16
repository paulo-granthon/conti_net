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


var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
