using Infrastructure.Repositories.BlogRespository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmallPost.Domain.Services.BlogService;

namespace SmallPost.Infrastructure
{
    public static class InfrastrcutureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureDBContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<WebContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("BlogContext")
                    ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<WebContext>();

            return services;
        }

        public static IServiceCollection AddInfrastructureDependeyGroup(this IServiceCollection services) 
        {
            services.AddScoped<IBlogRepository, BlogRepository>();

            return services;
        }
    }
}
