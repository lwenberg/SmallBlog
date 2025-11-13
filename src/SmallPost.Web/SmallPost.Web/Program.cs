using Infrastructure.Repositories.BlogRespository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmallPost.Infrastructure;
using SmallPost.Domain.Mappers;
using SmallPost.Domain.Services.BlogService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BlogContext")
        ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<WebContext>();

builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddAutoMapper(cfg => { }, typeof(BlogMapperProfile));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
app.MapRazorPages();
app.Run();
