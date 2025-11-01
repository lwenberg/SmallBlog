using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class WebContext : IdentityDbContext<IdentityUser>
    {
        public WebContext(DbContextOptions<WebContext> options)
         : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
    }
}
