using Microsoft.EntityFrameworkCore;
using Infrastructure.Services.BlogServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Infrastructure.WebContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BlogContext")
        ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));


builder.Services.AddScoped<IBlogService, BlogService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseAntiforgery();

app.Run();
