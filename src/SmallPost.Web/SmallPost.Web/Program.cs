using SmallPost.Domain.Mappers;
using SmallPost.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cfg => { }, typeof(BlogMapperProfile));
builder.Services
    .AddInfrastructureDBContext(builder.Configuration)
    .AddInfrastructureDependeyGroup();
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
